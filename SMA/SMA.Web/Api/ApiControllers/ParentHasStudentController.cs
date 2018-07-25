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
    public class ParentHasStudentController : ODataController
    {
        private readonly IParentHasStudentService _ParentHasStudentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ParentHasStudentController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IParentHasStudentService ParentHasStudentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ParentHasStudentService = ParentHasStudentService;
        }

        // GET: odata/ParentHasStudents
        [HttpGet]
        [Queryable]
        public IQueryable<ParentHasStudent> GetParentHasStudent()
        {
            
            var l= _ParentHasStudentService.Queryable().ToList();
            return _ParentHasStudentService.Queryable();
        }

        // GET: odata/ParentHasStudents(5)
        [Queryable]
        public SingleResult<ParentHasStudent> GetParentHasStudent([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_ParentHasStudentService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/ParentHasStudents(5)
        public async Task<IHttpActionResult> Put(Int64 key, ParentHasStudent ParentHasStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != ParentHasStudent.Id)
            {
                return BadRequest();
            }

            ParentHasStudent.ObjectState = ObjectState.Modified;
            _ParentHasStudentService.Update(ParentHasStudent);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentHasStudentExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(ParentHasStudent);
        }

        // POST: odata/ParentHasStudents
        public async Task<IHttpActionResult> Post(ParentHasStudent ParentHasStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ParentHasStudent.ObjectState = ObjectState.Added;
            _ParentHasStudentService.Insert(ParentHasStudent);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParentHasStudentExists(ParentHasStudent.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(ParentHasStudent);
        }

        //// PATCH: odata/ParentHasStudents(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<ParentHasStudent> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ParentHasStudent ParentHasStudent = await _ParentHasStudentService.FindAsync(key);

            if (ParentHasStudent == null)
            {
                return NotFound();
            }

            patch.Patch(ParentHasStudent);
            ParentHasStudent.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentHasStudentExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(ParentHasStudent);
        }

        // DELETE: odata/ParentHasStudents(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            ParentHasStudent ParentHasStudent = await _ParentHasStudentService.FindAsync(key);

            if (ParentHasStudent == null)
            {
                return NotFound();
            }

            ParentHasStudent.ObjectState = ObjectState.Deleted;

            _ParentHasStudentService.Delete(ParentHasStudent);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool ParentHasStudentExists(Int64 key)
        {
            return _ParentHasStudentService.Query(e => e.Id == key).Select().Any();
        }
    }
}