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
    public class MarksController : ApiController
    {
        private readonly IMarkService _MarkService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public MarksController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IMarkService MarkService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _MarkService = MarkService;
        }

        // Get All Marks
        [Route("api/Marks")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Mark> Marks = _MarkService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Marks);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Mark by Id
        [Route("api/Marks/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Mark Mark = _MarkService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Mark);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Mark
        [Route("api/Marks/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Mark value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _MarkService.Insert(value);
                    var response = Request.CreateResponse<Mark>(HttpStatusCode.Created, value);
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

        // Update Mark
        [Route("api/Marks/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Mark value)
        {
            try
            {
                value.Id = id;
                _MarkService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Mark value)
        {
            try
            {
                _MarkService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Mark
        [Route("api/Marks/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _MarkService.Delete(id);
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