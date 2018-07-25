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
    public class ParentHasStudentsController : ApiController
    {
        private readonly IParentHasStudentService _ParentHasStudentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ParentHasStudentsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IParentHasStudentService ParentHasStudentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ParentHasStudentService = ParentHasStudentService;
        }

        // Get All ParentHasStudents
        [Route("api/ParentHasStudents")]
        public IEnumerable<ParentHasStudent> Get()
        {
            return _ParentHasStudentService.Queryable().ToList();
        }

        // Get ParentHasStudent by Id
        [Route("api/ParentHasStudents/{id}")]
        public ParentHasStudent Get(int id)
        {
            try
            {
                ParentHasStudent ParentHasStudent = _ParentHasStudentService.Find(id);
                return ParentHasStudent;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound, ex.Message));
            }
        }

        // Insert ParentHasStudent
        [Route("api/ParentHasStudents/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]ParentHasStudent value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ParentHasStudentService.Insert(value);
                    var response = Request.CreateResponse<ParentHasStudent>(HttpStatusCode.Created, value);
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

        // Update ParentHasStudent
        [Route("api/ParentHasStudents/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, ParentHasStudent value)
        {
            try
            {
                value.Id = id;
                _ParentHasStudentService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, ParentHasStudent value)
        {
            try
            {
                _ParentHasStudentService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete ParentHasStudent
        [Route("api/ParentHasStudents/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _ParentHasStudentService.Delete(id);
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