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
    public class GroupsController : ApiController
    {
        private readonly IGroupService _GroupService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public GroupsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IGroupService GroupService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _GroupService = GroupService;
        }

        // Get All Groups
        [Route("api/Groups")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Group> Groups = _GroupService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Groups);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Group by Id
        [Route("api/Groups/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Group Group = _GroupService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Group);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Group
        [Route("api/Groups/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Group value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _GroupService.Insert(value);
                    var response = Request.CreateResponse<Group>(HttpStatusCode.Created, value);
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

        // Update Group
        [Route("api/Groups/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Group value)
        {
            try
            {
                value.Id = id;
                _GroupService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Group value)
        {
            try
            {
                _GroupService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Group
        [Route("api/Groups/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _GroupService.Delete(id);
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