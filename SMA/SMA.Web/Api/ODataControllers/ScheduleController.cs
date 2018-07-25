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
    public class ScheduleController : ODataController
    {
        private readonly IScheduleService _ScheduleService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ScheduleController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IScheduleService ScheduleService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ScheduleService = ScheduleService;
        }

        // GET: odata/Schedules
        [HttpGet]
        [Queryable]
        public IQueryable<Schedule> GetSchedule()
        {
            
            var l= _ScheduleService.Queryable().ToList();
            return _ScheduleService.Queryable();
        }

        // GET: odata/Schedules(5)
        [Queryable]
        public SingleResult<Schedule> GetSchedule([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_ScheduleService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Schedules(5)
        public async Task<IHttpActionResult> Put(Int64 key, Schedule Schedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Schedule.Id)
            {
                return BadRequest();
            }

            Schedule.ObjectState = ObjectState.Modified;
            _ScheduleService.Update(Schedule);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Schedule);
        }

        // POST: odata/Schedules
        public async Task<IHttpActionResult> Post(Schedule Schedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Schedule.ObjectState = ObjectState.Added;
            _ScheduleService.Insert(Schedule);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ScheduleExists(Schedule.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Schedule);
        }

        //// PATCH: odata/Schedules(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Schedule> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Schedule Schedule = await _ScheduleService.FindAsync(key);

            if (Schedule == null)
            {
                return NotFound();
            }

            patch.Patch(Schedule);
            Schedule.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Schedule);
        }

        // DELETE: odata/Schedules(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Schedule Schedule = await _ScheduleService.FindAsync(key);

            if (Schedule == null)
            {
                return NotFound();
            }

            Schedule.ObjectState = ObjectState.Deleted;

            _ScheduleService.Delete(Schedule);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool ScheduleExists(Int64 key)
        {
            return _ScheduleService.Query(e => e.Id == key).Select().Any();
        }
    }
}