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
    public class AssessmentController : ODataController
    {
        private readonly IAssessmentService _AssessmentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public AssessmentController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IAssessmentService AssessmentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _AssessmentService = AssessmentService;
        }

        // GET: odata/Assessments
        [HttpGet]
        public IQueryable<Assessment> GetAssessment()
        {
            
            var l= _AssessmentService.Queryable().ToList();
            return _AssessmentService.Queryable();
        }

        // GET: odata/Assessments(5)
        [Queryable]
        public SingleResult<Assessment> GetAssessment([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_AssessmentService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Assessments(5)
        public async Task<IHttpActionResult> Put(Int64 key, Assessment Assessment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Assessment.Id)
            {
                return BadRequest();
            }

            Assessment.ObjectState = ObjectState.Modified;
            _AssessmentService.Update(Assessment);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssessmentExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Assessment);
        }

        // POST: odata/Assessments
        public async Task<IHttpActionResult> Post(Assessment Assessment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Assessment.ObjectState = ObjectState.Added;
            _AssessmentService.Insert(Assessment);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AssessmentExists(Assessment.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Assessment);
        }

        //// PATCH: odata/Assessments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Assessment> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Assessment Assessment = await _AssessmentService.FindAsync(key);

            if (Assessment == null)
            {
                return NotFound();
            }

            patch.Patch(Assessment);
            Assessment.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssessmentExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Assessment);
        }

        // DELETE: odata/Assessments(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Assessment Assessment = await _AssessmentService.FindAsync(key);

            if (Assessment == null)
            {
                return NotFound();
            }

            Assessment.ObjectState = ObjectState.Deleted;

            _AssessmentService.Delete(Assessment);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool AssessmentExists(Int64 key)
        {
            return _AssessmentService.Query(e => e.Id == key).Select().Any();
        }
    }
}