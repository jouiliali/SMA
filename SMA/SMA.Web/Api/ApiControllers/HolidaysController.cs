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
    public class HolidaysController : ApiController
    {
        private readonly IHolidayService _HolidayService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public HolidaysController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IHolidayService HolidayService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _HolidayService = HolidayService;
        }

        // Get All Holidays
        [Route("api/Holidays")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Holiday> Holidays = _HolidayService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Holidays);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Holiday by Id
        [Route("api/Holidays/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Holiday Holiday = _HolidayService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Holiday);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Holiday
        [Route("api/Holidays/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Holiday value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _HolidayService.Insert(value);
                    var response = Request.CreateResponse<Holiday>(HttpStatusCode.Created, value);
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

        // Update Holiday
        [Route("api/Holidays/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Holiday value)
        {
            try
            {
                value.Id = id;
                _HolidayService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Holiday value)
        {
            try
            {
                _HolidayService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Holiday
        [Route("api/Holidays/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _HolidayService.Delete(id);
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