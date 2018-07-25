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
    public class TermsController : ApiController
    {
        private readonly ITermService _TermService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public TermsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ITermService TermService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _TermService = TermService;
        }

        // Get All Terms
        [Route("api/Terms")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Term> Terms = _TermService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Terms);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Term by Id
        [Route("api/Terms/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Term Term = _TermService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Term);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Term
        [Route("api/Terms/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Term value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _TermService.Insert(value);
                    var response = Request.CreateResponse<Term>(HttpStatusCode.Created, value);
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

        // Update Term
        [Route("api/Terms/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Term value)
        {
            try
            {
                value.Id = id;
                _TermService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Term value)
        {
            try
            {
                _TermService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Term
        [Route("api/Terms/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _TermService.Delete(id);
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