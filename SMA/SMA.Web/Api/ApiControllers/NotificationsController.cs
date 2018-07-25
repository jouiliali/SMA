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
    public class NotificationsController : ApiController
    {
        private readonly INotificationService _NotificationService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public NotificationsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            INotificationService NotificationService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _NotificationService = NotificationService;
        }

        // Get All Notifications
        [Route("api/Notifications")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Notification> Notifications = _NotificationService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Notifications);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Notification by Id
        [Route("api/Notifications/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Notification Notification = _NotificationService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Notification);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Notification
        [Route("api/Notifications/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Notification value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _NotificationService.Insert(value);
                    var response = Request.CreateResponse<Notification>(HttpStatusCode.Created, value);
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

        // Update Notification
        [Route("api/Notifications/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Notification value)
        {
            try
            {
                value.Id = id;
                _NotificationService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Notification value)
        {
            try
            {
                _NotificationService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Notification
        [Route("api/Notifications/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _NotificationService.Delete(id);
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