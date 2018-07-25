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
    public class FeedTypeController : ODataController
    {
        private readonly IFeedTypeService _FeedTypeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public FeedTypeController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IFeedTypeService FeedTypeService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _FeedTypeService = FeedTypeService;
        }

        // GET: odata/FeedTypes
        [HttpGet]
        [Queryable]
        public IQueryable<FeedType> GetFeedType()
        {
            
            var l= _FeedTypeService.Queryable().ToList();
            return _FeedTypeService.Queryable();
        }

        // GET: odata/FeedTypes(5)
        [Queryable]
        public SingleResult<FeedType> GetFeedType([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_FeedTypeService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/FeedTypes(5)
        public async Task<IHttpActionResult> Put(Int64 key, FeedType FeedType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != FeedType.Id)
            {
                return BadRequest();
            }

            FeedType.ObjectState = ObjectState.Modified;
            _FeedTypeService.Update(FeedType);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedTypeExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(FeedType);
        }

        // POST: odata/FeedTypes
        public async Task<IHttpActionResult> Post(FeedType FeedType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FeedType.ObjectState = ObjectState.Added;
            _FeedTypeService.Insert(FeedType);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FeedTypeExists(FeedType.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(FeedType);
        }

        //// PATCH: odata/FeedTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<FeedType> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FeedType FeedType = await _FeedTypeService.FindAsync(key);

            if (FeedType == null)
            {
                return NotFound();
            }

            patch.Patch(FeedType);
            FeedType.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedTypeExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(FeedType);
        }

        // DELETE: odata/FeedTypes(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            FeedType FeedType = await _FeedTypeService.FindAsync(key);

            if (FeedType == null)
            {
                return NotFound();
            }

            FeedType.ObjectState = ObjectState.Deleted;

            _FeedTypeService.Delete(FeedType);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool FeedTypeExists(Int64 key)
        {
            return _FeedTypeService.Query(e => e.Id == key).Select().Any();
        }
    }
}