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
    public class MarkController : ODataController
    {
        private readonly IMarkService _MarkService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public MarkController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IMarkService MarkService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _MarkService = MarkService;
        }

        // GET: odata/Marks
        [HttpGet]
        [Queryable]
        public IQueryable<Mark> GetMark()
        {
            
            var l= _MarkService.Queryable().ToList();
            return _MarkService.Queryable();
        }

        // GET: odata/Marks(5)
        [Queryable]
        public SingleResult<Mark> GetMark([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_MarkService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Marks(5)
        public async Task<IHttpActionResult> Put(Int64 key, Mark Mark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Mark.Id)
            {
                return BadRequest();
            }

            Mark.ObjectState = ObjectState.Modified;
            _MarkService.Update(Mark);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarkExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Mark);
        }

        // POST: odata/Marks
        public async Task<IHttpActionResult> Post(Mark Mark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mark.ObjectState = ObjectState.Added;
            _MarkService.Insert(Mark);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MarkExists(Mark.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Mark);
        }

        //// PATCH: odata/Marks(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Mark> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mark Mark = await _MarkService.FindAsync(key);

            if (Mark == null)
            {
                return NotFound();
            }

            patch.Patch(Mark);
            Mark.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarkExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Mark);
        }

        // DELETE: odata/Marks(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Mark Mark = await _MarkService.FindAsync(key);

            if (Mark == null)
            {
                return NotFound();
            }

            Mark.ObjectState = ObjectState.Deleted;

            _MarkService.Delete(Mark);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool MarkExists(Int64 key)
        {
            return _MarkService.Query(e => e.Id == key).Select().Any();
        }
    }
}