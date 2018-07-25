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
using System.Data.Entity;

namespace SMA.Web.Api
{
    [EnableCors("*", "*", "*")]
    public class SubjectsController : ApiController
    {
        private readonly ISubjectService _SubjectService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SubjectsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ISubjectService SubjectService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _SubjectService = SubjectService;
        }

        // Get All Subjects
        [Route("api/Subjects")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Subject> Subjects = _SubjectService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Subjects);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Subject by Id
        [Route("api/Subjects/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Subject Subject = _SubjectService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Subject);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Subject
        [Route("api/Subjects/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Subject value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _SubjectService.Insert(value);
                    var response = Request.CreateResponse<Subject>(HttpStatusCode.Created, value);
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

        // Update Subject
        [Route("api/Subjects/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Subject value)
        {
            try
            {
                value.Id = id;
                _SubjectService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Subject value)
        {
            try
            {
                _SubjectService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Subject
        [Route("api/Subjects/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _SubjectService.Delete(id);
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