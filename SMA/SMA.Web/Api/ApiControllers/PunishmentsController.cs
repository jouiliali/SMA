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
    public class PunishmentsController : ApiController
    {
        private readonly IPunishmentService _PunishmentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PunishmentsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IPunishmentService PunishmentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _PunishmentService = PunishmentService;
        }

        // Get All Punishments
        [Route("api/Punishments")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Punishment> Punishments = _PunishmentService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Punishments);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Punishment by Id
        [Route("api/Punishments/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Punishment Punishment = _PunishmentService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Punishment);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Punishment
        [Route("api/Punishments/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Punishment value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _PunishmentService.Insert(value);
                    var response = Request.CreateResponse<Punishment>(HttpStatusCode.Created, value);
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

        // Update Punishment
        [Route("api/Punishments/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Punishment value)
        {
            try
            {
                value.Id = id;
                _PunishmentService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Punishment value)
        {
            try
            {
                _PunishmentService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Punishment
        [Route("api/Punishments/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _PunishmentService.Delete(id);
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