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
    public class StudentController : ODataController
    {
        private readonly IStudentService _StudentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public StudentController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IStudentService StudentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _StudentService = StudentService;
        }

        // GET: odata/Students
        [HttpGet]
        [Queryable]
        public IQueryable<Student> GetStudent()
        {
            
            var l= _StudentService.Queryable().ToList();
            return _StudentService.Queryable();
        }

        // GET: odata/Students(5)
        [Queryable]
        public SingleResult<Student> GetStudent([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_StudentService.Queryable().Where(t => t.Id == key));
        }

        // GET: odata/Students(sam)
        [Queryable]
        public SingleResult<Student> GetStudent([FromODataUri] string FirstName)
        {
            return SingleResult.Create(_StudentService.Queryable().Where(t => t.FirstName == FirstName));
        }

        // PUT: odata/Students(5)
        public async Task<IHttpActionResult> Put(Int64 key, Student Student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Student.Id)
            {
                return BadRequest();
            }

            Student.ObjectState = ObjectState.Modified;
            _StudentService.Update(Student);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Student);
        }

        // POST: odata/Students
        public async Task<IHttpActionResult> Post(Student Student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Student.ObjectState = ObjectState.Added;
            _StudentService.Insert(Student);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentExists(Student.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Student);
        }

        //// PATCH: odata/Students(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Student> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Student Student = await _StudentService.FindAsync(key);

            if (Student == null)
            {
                return NotFound();
            }

            patch.Patch(Student);
            Student.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Student);
        }

        // DELETE: odata/Students(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Student Student = await _StudentService.FindAsync(key);

            if (Student == null)
            {
                return NotFound();
            }

            Student.ObjectState = ObjectState.Deleted;

            _StudentService.Delete(Student);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool StudentExists(Int64 key)
        {
            return _StudentService.Query(e => e.Id == key).Select().Any();
        }
    }
}