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
    public class EventController : ODataController
    {
        private readonly IEventService _EventService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public EventController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IEventService EventService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _EventService = EventService;
        }

        // GET: odata/Events
        [HttpGet]
        [Queryable]
        public IQueryable<Event> GetEvent()
        {
            
            var l= _EventService.Queryable().ToList();
            return _EventService.Queryable();
        }

        // GET: odata/Events(5)
        [Queryable]
        public SingleResult<Event> GetEvent([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_EventService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Events(5)
        public async Task<IHttpActionResult> Put(Int64 key, Event Event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Event.Id)
            {
                return BadRequest();
            }

            Event.ObjectState = ObjectState.Modified;
            _EventService.Update(Event);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Event);
        }

        // POST: odata/Events
        public async Task<IHttpActionResult> Post(Event Event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Event.ObjectState = ObjectState.Added;
            _EventService.Insert(Event);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EventExists(Event.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Event);
        }

        //// PATCH: odata/Events(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Event> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Event Event = await _EventService.FindAsync(key);

            if (Event == null)
            {
                return NotFound();
            }

            patch.Patch(Event);
            Event.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Event);
        }

        // DELETE: odata/Events(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Event Event = await _EventService.FindAsync(key);

            if (Event == null)
            {
                return NotFound();
            }

            Event.ObjectState = ObjectState.Deleted;

            _EventService.Delete(Event);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool EventExists(Int64 key)
        {
            return _EventService.Query(e => e.Id == key).Select().Any();
        }
    }
}