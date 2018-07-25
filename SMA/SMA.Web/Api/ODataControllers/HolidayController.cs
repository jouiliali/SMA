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
    public class HolidayController : ODataController
    {
        private readonly IHolidayService _HolidayService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public HolidayController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IHolidayService HolidayService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _HolidayService = HolidayService;
        }

        // GET: odata/Holidays
        [HttpGet]
        [Queryable]
        public IQueryable<Holiday> GetHoliday()
        {
            
            var l= _HolidayService.Queryable().ToList();
            return _HolidayService.Queryable();
        }

        // GET: odata/Holidays(5)
        [Queryable]
        public SingleResult<Holiday> GetHoliday([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_HolidayService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Holidays(5)
        public async Task<IHttpActionResult> Put(Int64 key, Holiday Holiday)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Holiday.Id)
            {
                return BadRequest();
            }

            Holiday.ObjectState = ObjectState.Modified;
            _HolidayService.Update(Holiday);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HolidayExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Holiday);
        }

        // POST: odata/Holidays
        public async Task<IHttpActionResult> Post(Holiday Holiday)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Holiday.ObjectState = ObjectState.Added;
            _HolidayService.Insert(Holiday);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HolidayExists(Holiday.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Holiday);
        }

        //// PATCH: odata/Holidays(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Holiday> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Holiday Holiday = await _HolidayService.FindAsync(key);

            if (Holiday == null)
            {
                return NotFound();
            }

            patch.Patch(Holiday);
            Holiday.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HolidayExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Holiday);
        }

        // DELETE: odata/Holidays(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Holiday Holiday = await _HolidayService.FindAsync(key);

            if (Holiday == null)
            {
                return NotFound();
            }

            Holiday.ObjectState = ObjectState.Deleted;

            _HolidayService.Delete(Holiday);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool HolidayExists(Int64 key)
        {
            return _HolidayService.Query(e => e.Id == key).Select().Any();
        }
    }
}