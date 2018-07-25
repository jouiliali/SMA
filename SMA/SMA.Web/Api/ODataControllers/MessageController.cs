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
    public class MessageController : ODataController
    {
        private readonly IMessageService _MessageService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public MessageController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IMessageService MessageService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _MessageService = MessageService;
        }

        // GET: odata/Messages
        [HttpGet]
        [Queryable]
        public IQueryable<Message> GetMessage()
        {
            
            var l= _MessageService.Queryable().ToList();
            return _MessageService.Queryable();
        }

        // GET: odata/Messages(5)
        [Queryable]
        public SingleResult<Message> GetMessage([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_MessageService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Messages(5)
        public async Task<IHttpActionResult> Put(Int64 key, Message Message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Message.Id)
            {
                return BadRequest();
            }

            Message.ObjectState = ObjectState.Modified;
            _MessageService.Update(Message);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Message);
        }

        // POST: odata/Messages
        public async Task<IHttpActionResult> Post(Message Message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Message.ObjectState = ObjectState.Added;
            _MessageService.Insert(Message);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MessageExists(Message.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Message);
        }

        //// PATCH: odata/Messages(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Message> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Message Message = await _MessageService.FindAsync(key);

            if (Message == null)
            {
                return NotFound();
            }

            patch.Patch(Message);
            Message.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Message);
        }

        // DELETE: odata/Messages(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Message Message = await _MessageService.FindAsync(key);

            if (Message == null)
            {
                return NotFound();
            }

            Message.ObjectState = ObjectState.Deleted;

            _MessageService.Delete(Message);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool MessageExists(Int64 key)
        {
            return _MessageService.Query(e => e.Id == key).Select().Any();
        }
    }
}