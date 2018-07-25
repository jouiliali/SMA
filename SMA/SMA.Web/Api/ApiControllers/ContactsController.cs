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
public class ContactsController : ApiController
    {
        private readonly IContactService _ContactService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ContactsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IContactService ContactService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ContactService = ContactService;
        }

        // Get All Contacts

        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Contact> Contacts = _ContactService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Contacts);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Contact by Id
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Contact Contact = _ContactService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Contact);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Contact
        public HttpResponseMessage Post(Contact value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ContactService.Insert(value);
                    var response = Request.CreateResponse<Contact>(HttpStatusCode.Created, value);
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

        // Update Contact
        public HttpResponseMessage Put(int id, Contact value)
        {
            try
            {
                value.Id = id;
                _ContactService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Contact value)
        {
            try
            {
                _ContactService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Contact
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _ContactService.Delete(id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}