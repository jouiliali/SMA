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
    public class PunishmentTypeController : ODataController
    {
        private readonly IPunishmentTypeService _PunishmentTypeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PunishmentTypeController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IPunishmentTypeService PunishmentTypeService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _PunishmentTypeService = PunishmentTypeService;
        }

        // GET: odata/PunishmentTypes
        [HttpGet]
        [Queryable]
        public IQueryable<PunishmentType> GetPunishmentType()
        {
            
            var l= _PunishmentTypeService.Queryable().ToList();
            return _PunishmentTypeService.Queryable();
        }

        // GET: odata/PunishmentTypes(5)
        [Queryable]
        public SingleResult<PunishmentType> GetPunishmentType([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_PunishmentTypeService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/PunishmentTypes(5)
        public async Task<IHttpActionResult> Put(Int64 key, PunishmentType PunishmentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != PunishmentType.Id)
            {
                return BadRequest();
            }

            PunishmentType.ObjectState = ObjectState.Modified;
            _PunishmentTypeService.Update(PunishmentType);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PunishmentTypeExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(PunishmentType);
        }

        // POST: odata/PunishmentTypes
        public async Task<IHttpActionResult> Post(PunishmentType PunishmentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PunishmentType.ObjectState = ObjectState.Added;
            _PunishmentTypeService.Insert(PunishmentType);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PunishmentTypeExists(PunishmentType.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(PunishmentType);
        }

        //// PATCH: odata/PunishmentTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<PunishmentType> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PunishmentType PunishmentType = await _PunishmentTypeService.FindAsync(key);

            if (PunishmentType == null)
            {
                return NotFound();
            }

            patch.Patch(PunishmentType);
            PunishmentType.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PunishmentTypeExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(PunishmentType);
        }

        // DELETE: odata/PunishmentTypes(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            PunishmentType PunishmentType = await _PunishmentTypeService.FindAsync(key);

            if (PunishmentType == null)
            {
                return NotFound();
            }

            PunishmentType.ObjectState = ObjectState.Deleted;

            _PunishmentTypeService.Delete(PunishmentType);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool PunishmentTypeExists(Int64 key)
        {
            return _PunishmentTypeService.Query(e => e.Id == key).Select().Any();
        }
    }
}