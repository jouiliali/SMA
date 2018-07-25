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
    public class TeacherController : ODataController
    {
        private readonly ITeacherService _TeacherService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public TeacherController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ITeacherService TeacherService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _TeacherService = TeacherService;
        }

        // GET: odata/Teachers
        [HttpGet]
        [Queryable]
        public IQueryable<Teacher> GetTeacher()
        {
            
            var l= _TeacherService.Queryable().ToList();
            return _TeacherService.Queryable();
        }

        // GET: odata/Teachers(5)
        [Queryable]
        public SingleResult<Teacher> GetTeacher([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_TeacherService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Teachers(5)
        public async Task<IHttpActionResult> Put(Int64 key, Teacher Teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Teacher.Id)
            {
                return BadRequest();
            }

            Teacher.ObjectState = ObjectState.Modified;
            _TeacherService.Update(Teacher);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Teacher);
        }

        // POST: odata/Teachers
        public async Task<IHttpActionResult> Post(Teacher Teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Teacher.ObjectState = ObjectState.Added;
            _TeacherService.Insert(Teacher);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TeacherExists(Teacher.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Teacher);
        }

        //// PATCH: odata/Teachers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Teacher> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Teacher Teacher = await _TeacherService.FindAsync(key);

            if (Teacher == null)
            {
                return NotFound();
            }

            patch.Patch(Teacher);
            Teacher.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Teacher);
        }

        // DELETE: odata/Teachers(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Teacher Teacher = await _TeacherService.FindAsync(key);

            if (Teacher == null)
            {
                return NotFound();
            }

            Teacher.ObjectState = ObjectState.Deleted;

            _TeacherService.Delete(Teacher);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool TeacherExists(Int64 key)
        {
            return _TeacherService.Query(e => e.Id == key).Select().Any();
        }
    }
}