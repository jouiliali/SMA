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

namespace SMA.Web.Api
{
    public class TestimonialController : ODataController
    {
        private readonly ITestimonialService _TestimonialService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public TestimonialController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ITestimonialService TestimonialService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _TestimonialService = TestimonialService;
        }

        // GET: odata/Testimonials
        [HttpGet]
        [Queryable]
        public IQueryable<Testimonial> GetTestimonial()
        {
            
            var l= _TestimonialService.Queryable().ToList();
            return _TestimonialService.Queryable();
        }

        // GET: odata/Testimonials(5)
        [Queryable]
        public SingleResult<Testimonial> GetTestimonial([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_TestimonialService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Testimonials(5)
        public async Task<IHttpActionResult> Put(Int64 key, Testimonial Testimonial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Testimonial.Id)
            {
                return BadRequest();
            }

            Testimonial.ObjectState = ObjectState.Modified;
            _TestimonialService.Update(Testimonial);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestimonialExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Testimonial);
        }

        // POST: odata/Testimonials
        public async Task<IHttpActionResult> Post(Testimonial Testimonial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Testimonial.ObjectState = ObjectState.Added;
            _TestimonialService.Insert(Testimonial);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TestimonialExists(Testimonial.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Testimonial);
        }

        //// PATCH: odata/Testimonials(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Testimonial> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Testimonial Testimonial = await _TestimonialService.FindAsync(key);

            if (Testimonial == null)
            {
                return NotFound();
            }

            patch.Patch(Testimonial);
            Testimonial.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestimonialExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Testimonial);
        }

        // DELETE: odata/Testimonials(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Testimonial Testimonial = await _TestimonialService.FindAsync(key);

            if (Testimonial == null)
            {
                return NotFound();
            }

            Testimonial.ObjectState = ObjectState.Deleted;

            _TestimonialService.Delete(Testimonial);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool TestimonialExists(Int64 key)
        {
            return _TestimonialService.Query(e => e.Id == key).Select().Any();
        }
    }
}