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
    public class StudentCommentController : ODataController
    {
        private readonly IStudentCommentService _StudentCommentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public StudentCommentController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IStudentCommentService StudentCommentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _StudentCommentService = StudentCommentService;
        }

        // GET: odata/StudentComments
        [HttpGet]
        [Queryable]
        public IQueryable<StudentComment> GetStudentComment()
        {
            
            var l= _StudentCommentService.Queryable().ToList();
            return _StudentCommentService.Queryable();
        }

        // GET: odata/StudentComments(5)
        [Queryable]
        public SingleResult<StudentComment> GetStudentComment([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_StudentCommentService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/StudentComments(5)
        public async Task<IHttpActionResult> Put(Int64 key, StudentComment StudentComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != StudentComment.Id)
            {
                return BadRequest();
            }

            StudentComment.ObjectState = ObjectState.Modified;
            _StudentCommentService.Update(StudentComment);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentCommentExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(StudentComment);
        }

        // POST: odata/StudentComments
        public async Task<IHttpActionResult> Post(StudentComment StudentComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentComment.ObjectState = ObjectState.Added;
            _StudentCommentService.Insert(StudentComment);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentCommentExists(StudentComment.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(StudentComment);
        }

        //// PATCH: odata/StudentComments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<StudentComment> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentComment StudentComment = await _StudentCommentService.FindAsync(key);

            if (StudentComment == null)
            {
                return NotFound();
            }

            patch.Patch(StudentComment);
            StudentComment.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentCommentExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(StudentComment);
        }

        // DELETE: odata/StudentComments(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            StudentComment StudentComment = await _StudentCommentService.FindAsync(key);

            if (StudentComment == null)
            {
                return NotFound();
            }

            StudentComment.ObjectState = ObjectState.Deleted;

            _StudentCommentService.Delete(StudentComment);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool StudentCommentExists(Int64 key)
        {
            return _StudentCommentService.Query(e => e.Id == key).Select().Any();
        }
    }
}