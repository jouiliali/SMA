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
    public class ParentController : ODataController
    {
        private readonly IParentService _ParentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ParentController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IParentService ParentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ParentService = ParentService;
        }

        // GET: odata/Parents
        [HttpGet]
        [Queryable]
        public IQueryable<Parent> GetParent()
        {
            
            var l= _ParentService.Queryable().ToList();
            return _ParentService.Queryable();
        }

        // GET: odata/Parents(5)
        [Queryable]
        public SingleResult<Parent> GetParent([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_ParentService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Parents(5)
        public async Task<IHttpActionResult> Put(Int64 key, Parent Parent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Parent.Id)
            {
                return BadRequest();
            }

            Parent.ObjectState = ObjectState.Modified;
            _ParentService.Update(Parent);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Parent);
        }

        // POST: odata/Parents
        public async Task<IHttpActionResult> Post(Parent Parent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Parent.ObjectState = ObjectState.Added;
            _ParentService.Insert(Parent);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParentExists(Parent.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Parent);
        }

        //// PATCH: odata/Parents(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Parent> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Parent Parent = await _ParentService.FindAsync(key);

            if (Parent == null)
            {
                return NotFound();
            }

            patch.Patch(Parent);
            Parent.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Parent);
        }

        // DELETE: odata/Parents(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Parent Parent = await _ParentService.FindAsync(key);

            if (Parent == null)
            {
                return NotFound();
            }

            Parent.ObjectState = ObjectState.Deleted;

            _ParentService.Delete(Parent);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool ParentExists(Int64 key)
        {
            return _ParentService.Query(e => e.Id == key).Select().Any();
        }
    }
}