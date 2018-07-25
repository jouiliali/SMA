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
    public class FeedsController : ApiController
    {
        private readonly IFeedService _FeedService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public FeedsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IFeedService FeedService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _FeedService = FeedService;
        }

        // Get All Feeds
        [Route("api/Feeds")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Feed> Feeds = _FeedService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Feeds);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Feed by Id
        [Route("api/Feeds/{id}")]
        [System.Web.Http.ActionName("Find")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Feed Feed = _FeedService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Feed);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Feed
        [Route("api/Feeds/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Feed value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _FeedService.Insert(value);
                    var response = Request.CreateResponse<Feed>(HttpStatusCode.Created, value);
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

        // Update Feed
        [Route("api/Feeds/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Feed value)
        {
            try
            {
                value.Id = id;
                _FeedService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Feed value)
        {
            try
            {
                _FeedService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Feed
        [Route("api/Feeds/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _FeedService.Delete(id);
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