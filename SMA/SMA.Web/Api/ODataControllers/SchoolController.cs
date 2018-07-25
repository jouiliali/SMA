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
    public class SchoolController : ODataController
    {
        private readonly ISchoolService _SchoolService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SchoolController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ISchoolService SchoolService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _SchoolService = SchoolService;
        }

        // GET: odata/Schools
        [HttpGet]
        [Queryable]
        public IQueryable<School> GetSchool()
        {
            
            var l= _SchoolService.Queryable().ToList();
            return _SchoolService.Queryable();
        }

        // GET: odata/Schools(5)
        [Queryable]
        public SingleResult<School> GetSchool([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_SchoolService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Schools(5)
        public async Task<IHttpActionResult> Put(Int64 key, School School)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != School.Id)
            {
                return BadRequest();
            }

            School.ObjectState = ObjectState.Modified;
            _SchoolService.Update(School);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(School);
        }

        // POST: odata/Schools
        public async Task<IHttpActionResult> Post(School School)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            School.ObjectState = ObjectState.Added;
            _SchoolService.Insert(School);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SchoolExists(School.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(School);
        }

        //// PATCH: odata/Schools(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<School> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            School School = await _SchoolService.FindAsync(key);

            if (School == null)
            {
                return NotFound();
            }

            patch.Patch(School);
            School.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(School);
        }

        // DELETE: odata/Schools(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            School School = await _SchoolService.FindAsync(key);

            if (School == null)
            {
                return NotFound();
            }

            School.ObjectState = ObjectState.Deleted;

            _SchoolService.Delete(School);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool SchoolExists(Int64 key)
        {
            return _SchoolService.Query(e => e.Id == key).Select().Any();
        }
    }
}