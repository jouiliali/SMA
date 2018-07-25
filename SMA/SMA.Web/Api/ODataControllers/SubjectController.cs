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
    public class SubjectController : ODataController
    {
        private readonly ISubjectService _SubjectService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SubjectController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ISubjectService SubjectService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _SubjectService = SubjectService;
        }

        // GET: odata/Subjects
        [HttpGet]
        [Queryable]
        public IQueryable<Subject> GetSubject()
        {
            
            var l= _SubjectService.Queryable().ToList();
            return _SubjectService.Queryable();
        }

        // GET: odata/Subjects(5)
        [Queryable]
        public SingleResult<Subject> GetSubject([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_SubjectService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Subjects(5)
        public async Task<IHttpActionResult> Put(Int64 key, Subject Subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Subject.Id)
            {
                return BadRequest();
            }

            Subject.ObjectState = ObjectState.Modified;
            _SubjectService.Update(Subject);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Subject);
        }

        // POST: odata/Subjects
        public async Task<IHttpActionResult> Post(Subject Subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Subject.ObjectState = ObjectState.Added;
            _SubjectService.Insert(Subject);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SubjectExists(Subject.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Subject);
        }

        //// PATCH: odata/Subjects(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Subject> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Subject Subject = await _SubjectService.FindAsync(key);

            if (Subject == null)
            {
                return NotFound();
            }

            patch.Patch(Subject);
            Subject.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Subject);
        }

        // DELETE: odata/Subjects(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Subject Subject = await _SubjectService.FindAsync(key);

            if (Subject == null)
            {
                return NotFound();
            }

            Subject.ObjectState = ObjectState.Deleted;

            _SubjectService.Delete(Subject);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool SubjectExists(Int64 key)
        {
            return _SubjectService.Query(e => e.Id == key).Select().Any();
        }
    }
}