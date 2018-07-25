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
    public class SchoolHeadsController : ApiController
    {
        private readonly ISchoolHeadService _SchoolHeadService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SchoolHeadsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ISchoolHeadService SchoolHeadService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _SchoolHeadService = SchoolHeadService;
        }

        // Get All SchoolHeads
        [Route("api/SchoolHeads")]
        public IEnumerable<SchoolHead> Get()
        {
            return _SchoolHeadService.Queryable().ToList();
        }

        // Get SchoolHead by Id
        [Route("api/SchoolHeads/{id}")]
        public SchoolHead Get(int id)
        {
            try
            {
                SchoolHead SchoolHead = _SchoolHeadService.Find(id);
                return SchoolHead;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound, ex.Message));
            }
        }

        // Insert SchoolHead
        [Route("api/SchoolHeads/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]SchoolHead value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _SchoolHeadService.Insert(value);
                    var response = Request.CreateResponse<SchoolHead>(HttpStatusCode.Created, value);
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

        // Update SchoolHead
        [Route("api/SchoolHeads/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, SchoolHead value)
        {
            try
            {
                value.Id = id;
                _SchoolHeadService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, SchoolHead value)
        {
            try
            {
                _SchoolHeadService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete SchoolHead
        [Route("api/SchoolHeads/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _SchoolHeadService.Delete(id);
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