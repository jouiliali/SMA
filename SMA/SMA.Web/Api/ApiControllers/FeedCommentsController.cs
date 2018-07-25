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
    public class FeedCommentsController : ApiController
    {
        private readonly IFeedCommentService _FeedCommentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public FeedCommentsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IFeedCommentService FeedCommentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _FeedCommentService = FeedCommentService;
        }

        // Get All FeedComments
        [Route("api/FeedComments")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<FeedComment> FeedComments = _FeedCommentService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, FeedComments);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get FeedComment by Id
        [Route("api/FeedComments/{id}")]
        [System.Web.Http.ActionName("Find")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                FeedComment FeedComment = _FeedCommentService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, FeedComment);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert FeedComment
        [Route("api/FeedComments/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]FeedComment value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _FeedCommentService.Insert(value);
                    var response = Request.CreateResponse<FeedComment>(HttpStatusCode.Created, value);
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

        // Update FeedComment
        [Route("api/FeedComments/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, FeedComment value)
        {
            try
            {
                value.Id = id;
                _FeedCommentService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, FeedComment value)
        {
            try
            {
                _FeedCommentService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete FeedComment
        [Route("api/FeedComments/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _FeedCommentService.Delete(id);
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