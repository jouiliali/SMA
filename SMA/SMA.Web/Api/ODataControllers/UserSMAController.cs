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
    public class UserSMAController : ODataController
    {
        private readonly IUserSMAService _UserSMAService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public UserSMAController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IUserSMAService UserSMAService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _UserSMAService = UserSMAService;
        }

        // GET: odata/UserSMAs
        [HttpGet]
        [Queryable]
        public IQueryable<UserSMA> GetUserSMA()
        {
            
            var l= _UserSMAService.Queryable().ToList();
            return _UserSMAService.Queryable();
        }

        // GET: odata/UserSMAs(5)
        [Queryable]
        public SingleResult<UserSMA> GetUserSMA([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_UserSMAService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/UserSMAs(5)
        public async Task<IHttpActionResult> Put(Int64 key, UserSMA UserSMA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != UserSMA.Id)
            {
                return BadRequest();
            }

            UserSMA.ObjectState = ObjectState.Modified;
            _UserSMAService.Update(UserSMA);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSMAExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(UserSMA);
        }

        // POST: odata/UserSMAs
        public async Task<IHttpActionResult> Post(UserSMA UserSMA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserSMA.ObjectState = ObjectState.Added;
            _UserSMAService.Insert(UserSMA);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserSMAExists(UserSMA.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(UserSMA);
        }

        //// PATCH: odata/UserSMAs(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<UserSMA> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserSMA UserSMA = await _UserSMAService.FindAsync(key);

            if (UserSMA == null)
            {
                return NotFound();
            }

            patch.Patch(UserSMA);
            UserSMA.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSMAExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(UserSMA);
        }

        // DELETE: odata/UserSMAs(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            UserSMA UserSMA = await _UserSMAService.FindAsync(key);

            if (UserSMA == null)
            {
                return NotFound();
            }

            UserSMA.ObjectState = ObjectState.Deleted;

            _UserSMAService.Delete(UserSMA);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool UserSMAExists(Int64 key)
        {
            return _UserSMAService.Query(e => e.Id == key).Select().Any();
        }
    }
}