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
    public class SchoolsController : ApiController
    {
        private readonly ISchoolService _SchoolService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SchoolsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ISchoolService SchoolService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _SchoolService = SchoolService;
        }

        // Get All Schools
        [Route("api/Schools")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<School> Schools = _SchoolService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Schools);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }


        // Get School by Id
        [Route("api/Schools/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                School School = _SchoolService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, School);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert School
        [Route("api/Schools/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]School value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _SchoolService.Insert(value);
                    var response = Request.CreateResponse<School>(HttpStatusCode.Created, value);
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

        // Update School
        [Route("api/Schools/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, School value)
        {
            try
            {
                value.Id = id;
                _SchoolService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, School value)
        {
            try
            {
                _SchoolService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete School
        [Route("api/Schools/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _SchoolService.Delete(id);
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