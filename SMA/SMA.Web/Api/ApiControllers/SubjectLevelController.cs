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
    public class SubjectLevelController : ODataController
    {
        private readonly ISubjectLevelService _SubjectLevelService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SubjectLevelController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ISubjectLevelService SubjectLevelService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _SubjectLevelService = SubjectLevelService;
        }

        // GET: odata/SubjectLevels
        [HttpGet]
        [Queryable]
        public IQueryable<SubjectLevel> GetSubjectLevel()
        {
            
            var l= _SubjectLevelService.Queryable().ToList();
            return _SubjectLevelService.Queryable();
        }

        // GET: odata/SubjectLevels(5)
        [Queryable]
        public SingleResult<SubjectLevel> GetSubjectLevel([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_SubjectLevelService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/SubjectLevels(5)
        public async Task<IHttpActionResult> Put(Int64 key, SubjectLevel SubjectLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != SubjectLevel.Id)
            {
                return BadRequest();
            }

            SubjectLevel.ObjectState = ObjectState.Modified;
            _SubjectLevelService.Update(SubjectLevel);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectLevelExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(SubjectLevel);
        }

        // POST: odata/SubjectLevels
        public async Task<IHttpActionResult> Post(SubjectLevel SubjectLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SubjectLevel.ObjectState = ObjectState.Added;
            _SubjectLevelService.Insert(SubjectLevel);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SubjectLevelExists(SubjectLevel.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(SubjectLevel);
        }

        //// PATCH: odata/SubjectLevels(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<SubjectLevel> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SubjectLevel SubjectLevel = await _SubjectLevelService.FindAsync(key);

            if (SubjectLevel == null)
            {
                return NotFound();
            }

            patch.Patch(SubjectLevel);
            SubjectLevel.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectLevelExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(SubjectLevel);
        }

        // DELETE: odata/SubjectLevels(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            SubjectLevel SubjectLevel = await _SubjectLevelService.FindAsync(key);

            if (SubjectLevel == null)
            {
                return NotFound();
            }

            SubjectLevel.ObjectState = ObjectState.Deleted;

            _SubjectLevelService.Delete(SubjectLevel);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool SubjectLevelExists(Int64 key)
        {
            return _SubjectLevelService.Query(e => e.Id == key).Select().Any();
        }
    }
}