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
using SMA.Utils;
namespace SMA.Web.Api
{
    [EnableCors("*", "*", "*")]
    public class UserSMAsController : ApiController
    {
        private readonly IUserSMAService _UserSMAService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public UserSMAsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IUserSMAService UserSMAService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _UserSMAService = UserSMAService;
        }

        // Get All UserSMAs
        [Route("api/UserSMAs")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<UserSMA> UserSMAs = _UserSMAService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, UserSMAs);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get UserSMA by Id
        [Route("api/UserSMAs/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                UserSMA UserSMA = _UserSMAService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, UserSMA);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get UserSMA by Id
        [HttpGet]
        [Route("api/UserSMAs/Login/{login}/{pwd}")]
        public HttpResponseMessage Login(string login, string pwd)
        {
            try
            {
                UserSMA UserSMA = _UserSMAService.Login(login, Hashing.Encrypt(pwd, true));
                if (UserSMA != null)
                {
                    UserSMA.Password = pwd;
                    return JsonConvert.JsonConvertObjectResult(Request, UserSMA);
                }
                return JsonConvert.JsonErrorResult(Request, "User not found!!");  
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert UserSMA
        [Route("api/UserSMAs/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]UserSMA value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _UserSMAService.Insert(value);
                    var response = Request.CreateResponse<UserSMA>(HttpStatusCode.Created, value);
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

        // Update UserSMA
        [Route("api/UserSMAs/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, UserSMA value)
        {
            try
            {
                value.Id = id;
                _UserSMAService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, UserSMA value)
        {
            try
            {
                _UserSMAService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete UserSMA
        [Route("api/UserSMAs/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _UserSMAService.Delete(id);
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