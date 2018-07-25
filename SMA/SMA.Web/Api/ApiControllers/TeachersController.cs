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
    public class TeachersController : ApiController
    {
        private readonly ITeacherService _TeacherService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public TeachersController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ITeacherService TeacherService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _TeacherService = TeacherService;
        }

        // Get All Teachers
        [Route("api/Teachers")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Teacher> Teachers = _TeacherService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Teachers);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Teacher by Id
        [Route("api/Teachers/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Teacher Teacher = _TeacherService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Teacher);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Teacher
        [Route("api/Teachers/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Teacher value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _TeacherService.Insert(value);
                    var response = Request.CreateResponse<Teacher>(HttpStatusCode.Created, value);
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

        // Update Teacher
        [Route("api/Teachers/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Teacher value)
        {
            try
            {
                value.Id = id;
                _TeacherService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Teacher value)
        {
            try
            {
                _TeacherService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Teacher
        [Route("api/Teachers/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _TeacherService.Delete(id);
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