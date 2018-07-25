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
    public class SchoolHeadController : ODataController
    {
        private readonly ISchoolHeadService _SchoolHeadService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SchoolHeadController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ISchoolHeadService SchoolHeadService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _SchoolHeadService = SchoolHeadService;
        }

        // GET: odata/SchoolHeads
        [HttpGet]
        [Queryable]
        public IQueryable<SchoolHead> GetSchoolHead()
        {
            
            var l= _SchoolHeadService.Queryable().ToList();
            return _SchoolHeadService.Queryable();
        }

        // GET: odata/SchoolHeads(5)
        [Queryable]
        public SingleResult<SchoolHead> GetSchoolHead([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_SchoolHeadService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/SchoolHeads(5)
        public async Task<IHttpActionResult> Put(Int64 key, SchoolHead SchoolHead)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != SchoolHead.Id)
            {
                return BadRequest();
            }

            SchoolHead.ObjectState = ObjectState.Modified;
            _SchoolHeadService.Update(SchoolHead);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolHeadExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(SchoolHead);
        }

        // POST: odata/SchoolHeads
        public async Task<IHttpActionResult> Post(SchoolHead SchoolHead)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SchoolHead.ObjectState = ObjectState.Added;
            _SchoolHeadService.Insert(SchoolHead);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SchoolHeadExists(SchoolHead.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(SchoolHead);
        }

        //// PATCH: odata/SchoolHeads(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<SchoolHead> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SchoolHead SchoolHead = await _SchoolHeadService.FindAsync(key);

            if (SchoolHead == null)
            {
                return NotFound();
            }

            patch.Patch(SchoolHead);
            SchoolHead.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolHeadExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(SchoolHead);
        }

        // DELETE: odata/SchoolHeads(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            SchoolHead SchoolHead = await _SchoolHeadService.FindAsync(key);

            if (SchoolHead == null)
            {
                return NotFound();
            }

            SchoolHead.ObjectState = ObjectState.Deleted;

            _SchoolHeadService.Delete(SchoolHead);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool SchoolHeadExists(Int64 key)
        {
            return _SchoolHeadService.Query(e => e.Id == key).Select().Any();
        }
    }
}