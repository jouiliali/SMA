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
    public class FeedCommentController : ODataController
    {
        private readonly IFeedCommentService _FeedCommentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public FeedCommentController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IFeedCommentService FeedCommentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _FeedCommentService = FeedCommentService;
        }

        // GET: odata/FeedComments
        [HttpGet]
        [Queryable]
        public IQueryable<FeedComment> GetFeedComment()
        {
            
            var l= _FeedCommentService.Queryable().ToList();
            return _FeedCommentService.Queryable();
        }

        // GET: odata/FeedComments(5)
        [Queryable]
        public SingleResult<FeedComment> GetFeedComment([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_FeedCommentService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/FeedComments(5)
        public async Task<IHttpActionResult> Put(Int64 key, FeedComment FeedComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != FeedComment.Id)
            {
                return BadRequest();
            }

            FeedComment.ObjectState = ObjectState.Modified;
            _FeedCommentService.Update(FeedComment);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedCommentExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(FeedComment);
        }

        // POST: odata/FeedComments
        public async Task<IHttpActionResult> Post(FeedComment FeedComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FeedComment.ObjectState = ObjectState.Added;
            _FeedCommentService.Insert(FeedComment);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FeedCommentExists(FeedComment.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(FeedComment);
        }

        //// PATCH: odata/FeedComments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<FeedComment> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FeedComment FeedComment = await _FeedCommentService.FindAsync(key);

            if (FeedComment == null)
            {
                return NotFound();
            }

            patch.Patch(FeedComment);
            FeedComment.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedCommentExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(FeedComment);
        }

        // DELETE: odata/FeedComments(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            FeedComment FeedComment = await _FeedCommentService.FindAsync(key);

            if (FeedComment == null)
            {
                return NotFound();
            }

            FeedComment.ObjectState = ObjectState.Deleted;

            _FeedCommentService.Delete(FeedComment);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool FeedCommentExists(Int64 key)
        {
            return _FeedCommentService.Query(e => e.Id == key).Select().Any();
        }
    }
}