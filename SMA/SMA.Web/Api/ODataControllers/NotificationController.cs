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
    public class NotificationController : ODataController
    {
        private readonly INotificationService _NotificationService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public NotificationController(
            IUnitOfWorkAsync unitOfWorkAsync,
            INotificationService NotificationService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _NotificationService = NotificationService;
        }

        // GET: odata/Notifications
        [HttpGet]
        [Queryable]
        public IQueryable<Notification> GetNotification()
        {
            
            var l= _NotificationService.Queryable().ToList();
            return _NotificationService.Queryable();
        }

        // GET: odata/Notifications(5)
        [Queryable]
        public SingleResult<Notification> GetNotification([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_NotificationService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Notifications(5)
        public async Task<IHttpActionResult> Put(Int64 key, Notification Notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Notification.Id)
            {
                return BadRequest();
            }

            Notification.ObjectState = ObjectState.Modified;
            _NotificationService.Update(Notification);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Notification);
        }

        // POST: odata/Notifications
        public async Task<IHttpActionResult> Post(Notification Notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Notification.ObjectState = ObjectState.Added;
            _NotificationService.Insert(Notification);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NotificationExists(Notification.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Notification);
        }

        //// PATCH: odata/Notifications(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Notification> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Notification Notification = await _NotificationService.FindAsync(key);

            if (Notification == null)
            {
                return NotFound();
            }

            patch.Patch(Notification);
            Notification.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Notification);
        }

        // DELETE: odata/Notifications(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Notification Notification = await _NotificationService.FindAsync(key);

            if (Notification == null)
            {
                return NotFound();
            }

            Notification.ObjectState = ObjectState.Deleted;

            _NotificationService.Delete(Notification);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool NotificationExists(Int64 key)
        {
            return _NotificationService.Query(e => e.Id == key).Select().Any();
        }
    }
}