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
    public class ClassRoomsController : ApiController
    {
        private readonly IClassRoomService _ClassRoomService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ClassRoomsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IClassRoomService ClassRoomService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ClassRoomService = ClassRoomService;
        }

        // Get All ClassRooms
        [Route("api/ClassRooms")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<ClassRoom> ClassRooms = _ClassRoomService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, ClassRooms);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get ClassRoom by Id
        [Route("api/ClassRooms/{id}")]
        [System.Web.Http.ActionName("Find")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                ClassRoom ClassRoom = _ClassRoomService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, ClassRoom);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert ClassRoom
        [Route("api/ClassRooms/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]ClassRoom value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ClassRoomService.Insert(value);
                    var response = Request.CreateResponse<ClassRoom>(HttpStatusCode.Created, value);
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

        // Update ClassRoom
        [Route("api/ClassRooms/modifier/{id}")]
        public async Task<HttpResponseMessage> PUT(int id, ClassRoom value)
        {
            try
            {
                value.Id = id;
                _ClassRoomService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, ClassRoom value)
        {
            try
            {
                _ClassRoomService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete ClassRoom
        [Route("api/ClassRooms/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _ClassRoomService.Delete(id);
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