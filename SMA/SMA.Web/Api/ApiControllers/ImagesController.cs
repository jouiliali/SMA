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
    public class ImagesController : ApiController
    {
        private readonly IImageService _ImageService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ImagesController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IImageService ImageService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ImageService = ImageService;
        }

        // Get All Images
        [Route("api/Images")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Image> Images = _ImageService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Images);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Image by Id
        [Route("api/Images/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Image Image = _ImageService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Image);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Image
        [Route("api/Images/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Image value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ImageService.Insert(value);
                    var response = Request.CreateResponse<Image>(HttpStatusCode.Created, value);
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

        // Update Image
        [Route("api/Images/modifier/{id}")]
        public async Task<HttpResponseMessage> PUT(int id, Image value)
        {
            try
            {
                value.Id = id;
                _ImageService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Image value)
        {
            try
            {
                _ImageService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Image
        [Route("api/Images/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _ImageService.Delete(id);
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