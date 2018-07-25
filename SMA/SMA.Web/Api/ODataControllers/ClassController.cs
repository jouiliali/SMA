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
    public class ClassController : ODataController
    {
        private readonly IClassService _ClassService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ClassController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IClassService ClassService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ClassService = ClassService;
        }

        // GET: odata/Classs
        [HttpGet]
        [Queryable]
        public IQueryable<Class> GetClass()
        {
            
            var l= _ClassService.Queryable().ToList();
            return _ClassService.Queryable();
        }

        // GET: odata/Classs(5)
        [Queryable]
        public SingleResult<Class> GetClass([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_ClassService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Classs(5)
        public async Task<IHttpActionResult> Put(Int64 key, Class Class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Class.Id)
            {
                return BadRequest();
            }

            Class.ObjectState = ObjectState.Modified;
            _ClassService.Update(Class);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Class);
        }

        // POST: odata/Classs
        public async Task<IHttpActionResult> Post(Class Class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Class.ObjectState = ObjectState.Added;
            _ClassService.Insert(Class);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClassExists(Class.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Class);
        }

        //// PATCH: odata/Classs(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Class> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Class Class = await _ClassService.FindAsync(key);

            if (Class == null)
            {
                return NotFound();
            }

            patch.Patch(Class);
            Class.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Class);
        }

        // DELETE: odata/Classs(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Class Class = await _ClassService.FindAsync(key);

            if (Class == null)
            {
                return NotFound();
            }

            Class.ObjectState = ObjectState.Deleted;

            _ClassService.Delete(Class);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool ClassExists(Int64 key)
        {
            return _ClassService.Query(e => e.Id == key).Select().Any();
        }
    }
}