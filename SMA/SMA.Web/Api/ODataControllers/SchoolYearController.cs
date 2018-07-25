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
    public class SchoolYearController : ODataController
    {
        private readonly ISchoolYearService _SchoolYearService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SchoolYearController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ISchoolYearService SchoolYearService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _SchoolYearService = SchoolYearService;
        }

        // GET: odata/SchoolYears
        [HttpGet]
        [Queryable]
        public IQueryable<SchoolYear> GetSchoolYear()
        {
            
            var l= _SchoolYearService.Queryable().ToList();
            return _SchoolYearService.Queryable();
        }

        // GET: odata/SchoolYears(5)
        [Queryable]
        public SingleResult<SchoolYear> GetSchoolYear([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_SchoolYearService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/SchoolYears(5)
        public async Task<IHttpActionResult> Put(Int64 key, SchoolYear SchoolYear)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != SchoolYear.Id)
            {
                return BadRequest();
            }

            SchoolYear.ObjectState = ObjectState.Modified;
            _SchoolYearService.Update(SchoolYear);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolYearExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(SchoolYear);
        }

        // POST: odata/SchoolYears
        public async Task<IHttpActionResult> Post(SchoolYear SchoolYear)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SchoolYear.ObjectState = ObjectState.Added;
            _SchoolYearService.Insert(SchoolYear);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SchoolYearExists(SchoolYear.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(SchoolYear);
        }

        //// PATCH: odata/SchoolYears(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<SchoolYear> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SchoolYear SchoolYear = await _SchoolYearService.FindAsync(key);

            if (SchoolYear == null)
            {
                return NotFound();
            }

            patch.Patch(SchoolYear);
            SchoolYear.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolYearExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(SchoolYear);
        }

        // DELETE: odata/SchoolYears(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            SchoolYear SchoolYear = await _SchoolYearService.FindAsync(key);

            if (SchoolYear == null)
            {
                return NotFound();
            }

            SchoolYear.ObjectState = ObjectState.Deleted;

            _SchoolYearService.Delete(SchoolYear);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool SchoolYearExists(Int64 key)
        {
            return _SchoolYearService.Query(e => e.Id == key).Select().Any();
        }
    }
}