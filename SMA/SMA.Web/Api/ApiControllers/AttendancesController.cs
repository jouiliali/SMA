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
    public class AttendancesController : ApiController
    {
        private readonly IAttendanceService _AttendanceService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public AttendancesController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IAttendanceService AttendanceService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _AttendanceService = AttendanceService;
        }

        // Get All Attendances
        [Route("api/Attendances")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Attendance> Attendances = _AttendanceService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Attendances);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Attendance by Id
        [Route("api/Attendances/{id}")]
        [System.Web.Http.ActionName("Find")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Attendance Attendance = _AttendanceService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Attendance);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Attendance
        [Route("api/Attendances/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Attendance value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _AttendanceService.Insert(value);
                    var response = Request.CreateResponse<Attendance>(HttpStatusCode.Created, value);
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

        // Update Attendance
        [Route("api/Attendances/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Attendance value)
        {
            try
            {
                value.Id = id;
                _AttendanceService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/Attendances/modifier/{name:alpha}")]
        public HttpResponseMessage Put(string Nom, Attendance value)
        {
            try
            {
                _AttendanceService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Attendance
        [Route("api/Attendances/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _AttendanceService.Delete(id);
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