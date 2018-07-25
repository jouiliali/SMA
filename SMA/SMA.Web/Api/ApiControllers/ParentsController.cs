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
    public class ParentsController : ApiController
    {
        private readonly IParentService _ParentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ParentsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IParentService ParentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ParentService = ParentService;
        }

        // Get All Parents
        [Route("api/Parents")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Parent> Parents = _ParentService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Parents);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Parent by Id
        [Route("api/Parents/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Parent Parent = _ParentService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Parent);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Parent
        [Route("api/Parents/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Parent value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ParentService.Insert(value);
                    var response = Request.CreateResponse<Parent>(HttpStatusCode.Created, value);
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

        // Update Parent
        [Route("api/Parents/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Parent value)
        {
            try
            {
                value.Id = id;
                _ParentService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Parent value)
        {
            try
            {
                _ParentService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Parent
        [Route("api/Parents/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _ParentService.Delete(id);
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