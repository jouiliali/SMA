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
    public class FeedController : ODataController
    {
        private readonly IFeedService _FeedService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public FeedController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IFeedService FeedService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _FeedService = FeedService;
        }

        // GET: odata/Feeds
        [HttpGet]
        [Queryable]
        public IQueryable<Feed> GetFeed()
        {
            
            var l= _FeedService.Queryable().ToList();
            return _FeedService.Queryable();
        }

        // GET: odata/Feeds(5)
        [Queryable]
        public SingleResult<Feed> GetFeed([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_FeedService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Feeds(5)
        public async Task<IHttpActionResult> Put(Int64 key, Feed Feed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Feed.Id)
            {
                return BadRequest();
            }

            Feed.ObjectState = ObjectState.Modified;
            _FeedService.Update(Feed);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Feed);
        }

        // POST: odata/Feeds
        public async Task<IHttpActionResult> Post(Feed Feed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Feed.ObjectState = ObjectState.Added;
            _FeedService.Insert(Feed);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FeedExists(Feed.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Feed);
        }

        //// PATCH: odata/Feeds(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Feed> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Feed Feed = await _FeedService.FindAsync(key);

            if (Feed == null)
            {
                return NotFound();
            }

            patch.Patch(Feed);
            Feed.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Feed);
        }

        // DELETE: odata/Feeds(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Feed Feed = await _FeedService.FindAsync(key);

            if (Feed == null)
            {
                return NotFound();
            }

            Feed.ObjectState = ObjectState.Deleted;

            _FeedService.Delete(Feed);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool FeedExists(Int64 key)
        {
            return _FeedService.Query(e => e.Id == key).Select().Any();
        }
    }
}