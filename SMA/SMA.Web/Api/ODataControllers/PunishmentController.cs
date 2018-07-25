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
    public class PunishmentController : ODataController
    {
        private readonly IPunishmentService _PunishmentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PunishmentController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IPunishmentService PunishmentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _PunishmentService = PunishmentService;
        }

        // GET: odata/Punishments
        [HttpGet]
        [Queryable]
        public IQueryable<Punishment> GetPunishment()
        {
            
            var l= _PunishmentService.Queryable().ToList();
            return _PunishmentService.Queryable();
        }

        // GET: odata/Punishments(5)
        [Queryable]
        public SingleResult<Punishment> GetPunishment([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_PunishmentService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Punishments(5)
        public async Task<IHttpActionResult> Put(Int64 key, Punishment Punishment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Punishment.Id)
            {
                return BadRequest();
            }

            Punishment.ObjectState = ObjectState.Modified;
            _PunishmentService.Update(Punishment);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PunishmentExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Punishment);
        }

        // POST: odata/Punishments
        public async Task<IHttpActionResult> Post(Punishment Punishment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Punishment.ObjectState = ObjectState.Added;
            _PunishmentService.Insert(Punishment);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PunishmentExists(Punishment.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Punishment);
        }

        //// PATCH: odata/Punishments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Punishment> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Punishment Punishment = await _PunishmentService.FindAsync(key);

            if (Punishment == null)
            {
                return NotFound();
            }

            patch.Patch(Punishment);
            Punishment.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PunishmentExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Punishment);
        }

        // DELETE: odata/Punishments(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Punishment Punishment = await _PunishmentService.FindAsync(key);

            if (Punishment == null)
            {
                return NotFound();
            }

            Punishment.ObjectState = ObjectState.Deleted;

            _PunishmentService.Delete(Punishment);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool PunishmentExists(Int64 key)
        {
            return _PunishmentService.Query(e => e.Id == key).Select().Any();
        }
    }
}