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
    public class FeedTypesController : ApiController
    {
        private readonly IFeedTypeService _FeedTypeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public FeedTypesController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IFeedTypeService FeedTypeService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _FeedTypeService = FeedTypeService;
        }

        // Get All FeedTypes
        [Route("api/FeedTypes")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<FeedType> FeedTypes = _FeedTypeService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, FeedTypes);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get FeedType by Id
        [Route("api/FeedTypes/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                FeedType FeedType = _FeedTypeService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, FeedType);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert FeedType
        [Route("api/FeedTypes/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]FeedType value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _FeedTypeService.Insert(value);
                    var response = Request.CreateResponse<FeedType>(HttpStatusCode.Created, value);
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

        // Update FeedType
        [Route("api/FeedTypes/modifier/{id}")]
        public async Task<HttpResponseMessage> PUT(int id, FeedType value)
        {
            try
            {
                value.Id = id;
                _FeedTypeService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, FeedType value)
        {
            try
            {
                _FeedTypeService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete FeedType
        [Route("api/FeedTypes/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _FeedTypeService.Delete(id);
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