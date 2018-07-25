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
    public class EventsController : ApiController
    {
        private readonly IEventService _EventService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public EventsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IEventService EventService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _EventService = EventService;
        }

        // Get All Events
        [Route("api/Events")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Event> Events = _EventService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Events);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Event by Id
        [Route("api/Events/{id}")]
        [System.Web.Http.ActionName("Find")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Event Event = _EventService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Event);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Event
        [Route("api/Events/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Event value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _EventService.Insert(value);
                    var response = Request.CreateResponse<Event>(HttpStatusCode.Created, value);
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

        // Update Event
        [Route("api/Events/modifier/{id}")]
        public async Task<HttpResponseMessage> PUT(int id, Event value)
        {
            try
            {
                value.Id = id;
                _EventService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Event value)
        {
            try
            {
                _EventService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Event
        [Route("api/Events/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _EventService.Delete(id);
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