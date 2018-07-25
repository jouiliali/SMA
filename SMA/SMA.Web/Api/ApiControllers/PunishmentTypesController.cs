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
    public class PunishmentTypesController : ApiController
    {
        private readonly IPunishmentTypeService _PunishmentTypeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PunishmentTypesController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IPunishmentTypeService PunishmentTypeService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _PunishmentTypeService = PunishmentTypeService;
        }

        // Get All PunishmentTypes
        [Route("api/PunishmentTypes")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<PunishmentType> PunishmentTypes = _PunishmentTypeService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, PunishmentTypes);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get PunishmentType by Id
        [Route("api/PunishmentTypes/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                PunishmentType PunishmentType = _PunishmentTypeService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, PunishmentType);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert PunishmentType
        [Route("api/PunishmentTypes/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]PunishmentType value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _PunishmentTypeService.Insert(value);
                    var response = Request.CreateResponse<PunishmentType>(HttpStatusCode.Created, value);
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

        // Update PunishmentType
        [Route("api/PunishmentTypes/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, PunishmentType value)
        {
            try
            {
                value.Id = id;
                _PunishmentTypeService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, PunishmentType value)
        {
            try
            {
                _PunishmentTypeService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete PunishmentType
        [Route("api/PunishmentTypes/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _PunishmentTypeService.Delete(id);
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