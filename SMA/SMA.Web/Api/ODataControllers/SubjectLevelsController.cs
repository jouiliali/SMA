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
    public class SubjectLevelsController : ApiController
    {
        private readonly ISubjectLevelService _SubjectLevelService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SubjectLevelsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ISubjectLevelService SubjectLevelService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _SubjectLevelService = SubjectLevelService;
        }

        // Get All SubjectLevels
        [Route("api/SubjectLevels")]
        public IEnumerable<SubjectLevel> Get()
        {
            //return _SubjectLevelService.Queryable().ToList();
            return _SubjectLevelService.GetSubjectLevelsWithSubLevels().ToList();
        }

        // Get SubjectLevel by Id
        [Route("api/SubjectLevels/{id}")]
        public SubjectLevel Get(int id)
        {
            try
            {
                SubjectLevel SubjectLevel = _SubjectLevelService.Find(id);
                return SubjectLevel;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound, ex.Message));
            }
        }

        // Insert SubjectLevel
        [Route("api/SubjectLevels/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]SubjectLevel value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _SubjectLevelService.Insert(value);
                    var response = Request.CreateResponse<SubjectLevel>(HttpStatusCode.Created, value);
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

        // Update SubjectLevel
        [Route("api/SubjectLevels/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, SubjectLevel value)
        {
            try
            {
                value.Id = id;
                _SubjectLevelService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, SubjectLevel value)
        {
            try
            {
                _SubjectLevelService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete SubjectLevel
        [Route("api/SubjectLevels/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _SubjectLevelService.Delete(id);
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