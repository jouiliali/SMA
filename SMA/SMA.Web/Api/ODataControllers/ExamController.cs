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
    public class ExamController : ODataController
    {
        private readonly IExamService _ExamService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ExamController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IExamService ExamService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ExamService = ExamService;
        }

        // GET: odata/Exams
        [HttpGet]
        [Queryable]
        public IQueryable<Exam> GetExam()
        {
            
            var l= _ExamService.Queryable().ToList();
            return _ExamService.Queryable();
        }

        // GET: odata/Exams(5)
        [Queryable]
        public SingleResult<Exam> GetExam([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_ExamService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Exams(5)
        public async Task<IHttpActionResult> Put(Int64 key, Exam Exam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Exam.Id)
            {
                return BadRequest();
            }

            Exam.ObjectState = ObjectState.Modified;
            _ExamService.Update(Exam);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Exam);
        }

        // POST: odata/Exams
        public async Task<IHttpActionResult> Post(Exam Exam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Exam.ObjectState = ObjectState.Added;
            _ExamService.Insert(Exam);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExamExists(Exam.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Exam);
        }

        //// PATCH: odata/Exams(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Exam> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Exam Exam = await _ExamService.FindAsync(key);

            if (Exam == null)
            {
                return NotFound();
            }

            patch.Patch(Exam);
            Exam.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Exam);
        }

        // DELETE: odata/Exams(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Exam Exam = await _ExamService.FindAsync(key);

            if (Exam == null)
            {
                return NotFound();
            }

            Exam.ObjectState = ObjectState.Deleted;

            _ExamService.Delete(Exam);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool ExamExists(Int64 key)
        {
            return _ExamService.Query(e => e.Id == key).Select().Any();
        }
    }
}