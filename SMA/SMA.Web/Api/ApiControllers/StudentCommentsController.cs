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
    public class StudentCommentsController : ApiController
    {
        private readonly IStudentCommentService _StudentCommentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public StudentCommentsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IStudentCommentService StudentCommentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _StudentCommentService = StudentCommentService;
        }

        // Get All StudentComments
        [Route("api/StudentComments")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<StudentComment> StudentComments = _StudentCommentService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, StudentComments);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get StudentComment by Id
        [Route("api/StudentComments/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                StudentComment StudentComment = _StudentCommentService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, StudentComment);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert StudentComment
        [Route("api/StudentComments/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]StudentComment value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _StudentCommentService.Insert(value);
                    var response = Request.CreateResponse<StudentComment>(HttpStatusCode.Created, value);
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

        // Update StudentComment
        [Route("api/StudentComments/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, StudentComment value)
        {
            try
            {
                value.Id = id;
                _StudentCommentService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, StudentComment value)
        {
            try
            {
                _StudentCommentService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete StudentComment
        [Route("api/StudentComments/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _StudentCommentService.Delete(id);
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