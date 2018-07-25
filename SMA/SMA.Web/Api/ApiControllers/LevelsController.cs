using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using SMA.Entities.Models;
using SMA.Service;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Cors;

namespace SMA.Web.Api
{
    [EnableCors("*", "*", "*")]
    public class LevelsController : ApiController
    {
        private readonly ILevelService _LevelService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public LevelsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ILevelService LevelService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _LevelService = LevelService;
        }

        // Get All Levels
        [Route("api/Levels")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Level> Levels = _LevelService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Levels);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Level by Id
        [Route("api/Levels/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Level Level = _LevelService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Level);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Level
        [Route("api/Levels/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Level value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _LevelService.Insert(value);
                    var response = Request.CreateResponse<Level>(HttpStatusCode.Created, value);
                    await _unitOfWorkAsync.SaveChangesAsync();
                    return response;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Model state is invalid");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Update Level
        [Route("api/Levels/modifier/{id}")]
        public async Task<HttpResponseMessage> PUT(int id, Level value)
        {
            try
            {
                value.Id = id;
                _LevelService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Level value)
        {
            try
            {
                _LevelService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Level
        [Route("api/Levels/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _LevelService.Delete(id);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}