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
    public class SchoolYearsController : ApiController
    {
        private readonly ISchoolYearService _SchoolYearService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SchoolYearsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ISchoolYearService SchoolYearService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _SchoolYearService = SchoolYearService;
        }

        // Get All SchoolYears
        [Route("api/SchoolYears")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<SchoolYear> SchoolYears = _SchoolYearService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, SchoolYears);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get SchoolYear by Id
        [Route("api/SchoolYears/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                SchoolYear SchoolYear = _SchoolYearService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, SchoolYear);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert SchoolYear
        [Route("api/SchoolYears/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]SchoolYear value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _SchoolYearService.Insert(value);
                    var response = Request.CreateResponse<SchoolYear>(HttpStatusCode.Created, value);
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

        // Update SchoolYear
        [Route("api/SchoolYears/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, SchoolYear value)
        {
            try
            {
                value.Id = id;
                _SchoolYearService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, SchoolYear value)
        {
            try
            {
                _SchoolYearService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete SchoolYear
        [Route("api/SchoolYears/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _SchoolYearService.Delete(id);
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