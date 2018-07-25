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
    public class ExamsController : ApiController
    {
        private readonly IExamService _ExamService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ExamsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IExamService ExamService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ExamService = ExamService;
        }

        // Get All Exams
        [Route("api/Exams")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Exam> Exams = _ExamService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Exams);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Exam by Id
        [Route("api/Exams/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Exam Exam = _ExamService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Exam);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Exam
        [Route("api/Exams/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Exam value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ExamService.Insert(value);
                    var response = Request.CreateResponse<Exam>(HttpStatusCode.Created, value);
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

        // Update Exam
        [Route("api/Exams/modifier/{id}")]
        public async Task<HttpResponseMessage> PUT(int id, Exam value)
        {
            try
            {
                value.Id = id;
                _ExamService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Exam value)
        {
            try
            {
                _ExamService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Exam
        [Route("api/Exams/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _ExamService.Delete(id);
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