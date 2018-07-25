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
    public class MessagesController : ApiController
    {
        private readonly IMessageService _MessageService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public MessagesController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IMessageService MessageService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _MessageService = MessageService;
        }

        // Get All Messages
        [Route("api/Messages")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Message> Messages = _MessageService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Messages);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Message by Id
        [Route("api/Messages/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Message Message = _MessageService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Message);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Message
        [Route("api/Messages/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Message value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _MessageService.Insert(value);
                    var response = Request.CreateResponse<Message>(HttpStatusCode.Created, value);
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

        // Update Message
        [Route("api/Messages/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Message value)
        {
            try
            {
                value.Id = id;
                _MessageService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Message value)
        {
            try
            {
                _MessageService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Message
        [Route("api/Messages/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _MessageService.Delete(id);
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