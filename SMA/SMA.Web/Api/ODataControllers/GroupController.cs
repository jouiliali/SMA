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
    public class GroupController : ODataController
    {
        private readonly IGroupService _GroupService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public GroupController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IGroupService GroupService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _GroupService = GroupService;
        }

        // GET: odata/Groups
        [HttpGet]
        [Queryable]
        public IQueryable<Group> GetGroup()
        {
            
            var l= _GroupService.Queryable().ToList();
            return _GroupService.Queryable();
        }

        // GET: odata/Groups(5)
        [Queryable]
        public SingleResult<Group> GetGroup([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_GroupService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Groups(5)
        public async Task<IHttpActionResult> Put(Int64 key, Group Group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Group.Id)
            {
                return BadRequest();
            }

            Group.ObjectState = ObjectState.Modified;
            _GroupService.Update(Group);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Group);
        }

        // POST: odata/Groups
        public async Task<IHttpActionResult> Post(Group Group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Group.ObjectState = ObjectState.Added;
            _GroupService.Insert(Group);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GroupExists(Group.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Group);
        }

        //// PATCH: odata/Groups(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Group> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Group Group = await _GroupService.FindAsync(key);

            if (Group == null)
            {
                return NotFound();
            }

            patch.Patch(Group);
            Group.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Group);
        }

        // DELETE: odata/Groups(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Group Group = await _GroupService.FindAsync(key);

            if (Group == null)
            {
                return NotFound();
            }

            Group.ObjectState = ObjectState.Deleted;

            _GroupService.Delete(Group);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool GroupExists(Int64 key)
        {
            return _GroupService.Query(e => e.Id == key).Select().Any();
        }
    }
}