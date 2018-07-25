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
    public class SchedulesController : ApiController
    {
        private readonly IScheduleService _ScheduleService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SchedulesController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IScheduleService ScheduleService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ScheduleService = ScheduleService;
        }

        // Get All Schedules
        [Route("api/Schedules")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Schedule> Schedules = _ScheduleService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Schedules);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Schedule by Id
        [Route("api/Schedules/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Schedule Schedule = _ScheduleService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Schedule);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Schedule
        [Route("api/Schedules/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Schedule value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ScheduleService.Insert(value);
                    var response = Request.CreateResponse<Schedule>(HttpStatusCode.Created, value);
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

        // Update Schedule
        [Route("api/Schedules/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Schedule value)
        {
            try
            {
                value.Id = id;
                _ScheduleService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Schedule value)
        {
            try
            {
                _ScheduleService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Schedule
        [Route("api/Schedules/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _ScheduleService.Delete(id);
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