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
    public class StudentsController : ApiController
    {
        private readonly IStudentService _StudentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private static SMAContext cc = new SMAContext();

        public StudentsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IStudentService StudentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _StudentService = StudentService;
        }

        // Get All Students
       // [Route("api/Students")]
       // [System.Web.Http.ActionName("All")]
        //[HttpGet]
        [Route("api/Students")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Student> students = _StudentService.GetStudentsWithClass().ToList();
                return JsonConvert.JsonConvertListResult(Request, students);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Student by Id
        [Route("api/Students/{id}")]
        [System.Web.Http.ActionName("Find")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Student Student = _StudentService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Student);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

         //Get Student by FirstName
        [Route("api/Students/{name:alpha}")]
        [System.Web.Http.ActionName("Find")]
        public HttpResponseMessage Get(string name)
        {
            try
            {
                IEnumerable<Student> students = _StudentService.GetStudentsByName(name).ToList();
                return JsonConvert.JsonConvertListResult(Request, students);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }
            

        // Insert Student
       
        //[System.Web.Http.ActionName("Create")]
       //[HttpPost]
       //[System.Web.Http.AcceptVerbs("OPTIONS")]
       [Route("api/Students/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Student value)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    _StudentService.Insert(value);
                    var response = Request.CreateResponse<Student>(HttpStatusCode.Created, value);
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

        // Update Student
       [Route("api/Students/modifier/{id}")]
        public async Task<HttpResponseMessage> PUT(int id, Student value)
        {
            try
            {
                value.Id = id; 
                _StudentService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                //await _unitOfWorkAsync.Rollback();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //[Route("api/Students/modifier/{name:alpha}")]
        //public HttpResponseMessage Put(string Nom, Student value)
        //{
        //    try
        //    {
        //        _StudentService.Update(value);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        // Delete Student
        [Route("api/Students/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _StudentService.Delete(id);
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