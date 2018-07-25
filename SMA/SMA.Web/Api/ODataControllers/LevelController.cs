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
    public class LevelController : ODataController
    {
        private readonly ILevelService _LevelService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public LevelController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ILevelService LevelService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _LevelService = LevelService;
        }

        // GET: odata/Levels
        [HttpGet]
        [Queryable]
        public IQueryable<Level> GetLevel()
        {
            
            var l= _LevelService.Queryable().ToList();
            return _LevelService.Queryable();
        }

        // GET: odata/Levels(5)
        [Queryable]
        public SingleResult<Level> GetLevel([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_LevelService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Levels(5)
        public async Task<IHttpActionResult> Put(Int64 key, Level Level)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Level.Id)
            {
                return BadRequest();
            }

            Level.ObjectState = ObjectState.Modified;
            _LevelService.Update(Level);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LevelExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Level);
        }

        // POST: odata/Levels
        public async Task<IHttpActionResult> Post(Level Level)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Level.ObjectState = ObjectState.Added;
            _LevelService.Insert(Level);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LevelExists(Level.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Level);
        }

        //// PATCH: odata/Levels(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Level> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Level Level = await _LevelService.FindAsync(key);

            if (Level == null)
            {
                return NotFound();
            }

            patch.Patch(Level);
            Level.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LevelExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Level);
        }

        // DELETE: odata/Levels(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Level Level = await _LevelService.FindAsync(key);

            if (Level == null)
            {
                return NotFound();
            }

            Level.ObjectState = ObjectState.Deleted;

            _LevelService.Delete(Level);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool LevelExists(Int64 key)
        {
            return _LevelService.Query(e => e.Id == key).Select().Any();
        }
    }
}