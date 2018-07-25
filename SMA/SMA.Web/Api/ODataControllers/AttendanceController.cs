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
    public class AttendanceController : ODataController
    {
        private readonly IAttendanceService _AttendanceService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public AttendanceController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IAttendanceService AttendanceService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _AttendanceService = AttendanceService;
        }

        // GET: odata/Attendances
        [HttpGet]
        [Queryable]
        public IQueryable<Attendance> GetAttendance()
        {
            
            var l= _AttendanceService.Queryable().ToList();
            return _AttendanceService.Queryable();
        }

        // GET: odata/Attendances(5)
        [Queryable]
        public SingleResult<Attendance> GetAttendance([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_AttendanceService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Attendances(5)
        public async Task<IHttpActionResult> Put(Int64 key, Attendance Attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Attendance.Id)
            {
                return BadRequest();
            }

            Attendance.ObjectState = ObjectState.Modified;
            _AttendanceService.Update(Attendance);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Attendance);
        }

        // POST: odata/Attendances
        public async Task<IHttpActionResult> Post(Attendance Attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Attendance.ObjectState = ObjectState.Added;
            _AttendanceService.Insert(Attendance);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AttendanceExists(Attendance.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Attendance);
        }

        //// PATCH: odata/Attendances(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Attendance> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Attendance Attendance = await _AttendanceService.FindAsync(key);

            if (Attendance == null)
            {
                return NotFound();
            }

            patch.Patch(Attendance);
            Attendance.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Attendance);
        }

        // DELETE: odata/Attendances(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Attendance Attendance = await _AttendanceService.FindAsync(key);

            if (Attendance == null)
            {
                return NotFound();
            }

            Attendance.ObjectState = ObjectState.Deleted;

            _AttendanceService.Delete(Attendance);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool AttendanceExists(Int64 key)
        {
            return _AttendanceService.Query(e => e.Id == key).Select().Any();
        }
    }
}