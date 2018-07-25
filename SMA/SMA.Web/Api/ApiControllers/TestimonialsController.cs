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
    public class TestimonialsController : ApiController
    {
        private readonly ITestimonialService _TestimonialService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public TestimonialsController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ITestimonialService TestimonialService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _TestimonialService = TestimonialService;
        }

        // Get All Testimonials
        [Route("api/Testimonials")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Testimonial> Testimonials = _TestimonialService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Testimonials);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Testimonial by Id
        [Route("api/Testimonials/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Testimonial Testimonial = _TestimonialService.Find(id);
                return JsonConvert.JsonConvertObjectResult(Request, Testimonial);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Insert Testimonial
        [Route("api/Testimonials/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Testimonial value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _TestimonialService.Insert(value);
                    var response = Request.CreateResponse<Testimonial>(HttpStatusCode.Created, value);
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

        // Update Testimonial
        [Route("api/Testimonials/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Testimonial value)
        {
            try
            {
                value.Id = id;
                _TestimonialService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(string Nom, Testimonial value)
        {
            try
            {
                _TestimonialService.Update(value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete Testimonial
        [Route("api/Testimonials/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _TestimonialService.Delete(id);
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