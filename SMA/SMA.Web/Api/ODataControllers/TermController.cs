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
    public class TermController : ODataController
    {
        private readonly ITermService _TermService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public TermController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ITermService TermService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _TermService = TermService;
        }

        // GET: odata/Terms
        [HttpGet]
        [Queryable]
        public IQueryable<Term> GetTerm()
        {
            
            var l= _TermService.Queryable().ToList();
            return _TermService.Queryable();
        }

        // GET: odata/Terms(5)
        [Queryable]
        public SingleResult<Term> GetTerm([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_TermService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Terms(5)
        public async Task<IHttpActionResult> Put(Int64 key, Term Term)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Term.Id)
            {
                return BadRequest();
            }

            Term.ObjectState = ObjectState.Modified;
            _TermService.Update(Term);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TermExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Term);
        }

        // POST: odata/Terms
        public async Task<IHttpActionResult> Post(Term Term)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Term.ObjectState = ObjectState.Added;
            _TermService.Insert(Term);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TermExists(Term.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Term);
        }

        //// PATCH: odata/Terms(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Term> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Term Term = await _TermService.FindAsync(key);

            if (Term == null)
            {
                return NotFound();
            }

            patch.Patch(Term);
            Term.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TermExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Term);
        }

        // DELETE: odata/Terms(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Term Term = await _TermService.FindAsync(key);

            if (Term == null)
            {
                return NotFound();
            }

            Term.ObjectState = ObjectState.Deleted;

            _TermService.Delete(Term);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool TermExists(Int64 key)
        {
            return _TermService.Query(e => e.Id == key).Select().Any();
        }
    }
}