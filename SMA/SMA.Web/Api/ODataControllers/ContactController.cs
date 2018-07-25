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
    public class ContactController : ODataController
    {
        private readonly IContactService _ContactService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ContactController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IContactService ContactService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ContactService = ContactService;
        }

        // GET: odata/Contacts
        [HttpGet]
        [Queryable]
        public IQueryable<Contact> GetContact()
        {
            
            var l= _ContactService.Queryable().ToList();
            return _ContactService.Queryable();
        }

        // GET: odata/Contacts(5)
        [Queryable]
        public SingleResult<Contact> GetContact([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_ContactService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Contacts(5)
        public async Task<IHttpActionResult> Put(Int64 key, Contact Contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Contact.Id)
            {
                return BadRequest();
            }

            Contact.ObjectState = ObjectState.Modified;
            _ContactService.Update(Contact);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Contact);
        }

        // POST: odata/Contacts
        public async Task<IHttpActionResult> Post(Contact Contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contact.ObjectState = ObjectState.Added;
            _ContactService.Insert(Contact);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContactExists(Contact.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Contact);
        }

        //// PATCH: odata/Contacts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Contact> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contact Contact = await _ContactService.FindAsync(key);

            if (Contact == null)
            {
                return NotFound();
            }

            patch.Patch(Contact);
            Contact.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Contact);
        }

        // DELETE: odata/Contacts(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Contact Contact = await _ContactService.FindAsync(key);

            if (Contact == null)
            {
                return NotFound();
            }

            Contact.ObjectState = ObjectState.Deleted;

            _ContactService.Delete(Contact);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool ContactExists(Int64 key)
        {
            return _ContactService.Query(e => e.Id == key).Select().Any();
        }
    }
}