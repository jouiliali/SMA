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
    public class AssessmentsController : ApiController
    {
        private readonly IAssessmentService _AssessmentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public AssessmentsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IAssessmentService AssessmentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _AssessmentService = AssessmentService;
        }

        // Get All Assessments
        [Route("api/Assessments")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Assessment> Assessments = _AssessmentService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Assessments);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Assessment by Id
        [Route("api/Assessments/{id}")]
        [System.Web.Http.ActionName("Find")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Assessment Assessment = _AssessmentService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Assessment);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Assessment
        [Route("api/Assessments/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Assessment value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _AssessmentService.Insert(value);
                    var response = Request.CreateResponse<Assessment>(HttpStatusCode.Created, value);
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

        // Update Assessment
        [Route("api/Assessments/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Assessment value)
        {
            try
            {
                value.Id = id;
                _AssessmentService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/Assessments/modifier/{name:alpha}")]
        public HttpResponseMessage Put(string Nom, Assessment value)
        {
            try
            {
                _AssessmentService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Assessment
        [Route("api/Assessments/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _AssessmentService.Delete(id);
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