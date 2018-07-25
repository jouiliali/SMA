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
    public class ClassRoomController : ODataController
    {
        private readonly IClassRoomService _ClassRoomService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ClassRoomController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IClassRoomService ClassRoomService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ClassRoomService = ClassRoomService;
        }

        // GET: odata/ClassRooms
        [HttpGet]
        [Queryable]
        public IQueryable<ClassRoom> GetClassRoom()
        {
            
            var l= _ClassRoomService.Queryable().ToList();
            return _ClassRoomService.Queryable();
        }

        // GET: odata/ClassRooms(5)
        [Queryable]
        public SingleResult<ClassRoom> GetClassRoom([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_ClassRoomService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/ClassRooms(5)
        public async Task<IHttpActionResult> Put(Int64 key, ClassRoom ClassRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != ClassRoom.Id)
            {
                return BadRequest();
            }

            ClassRoom.ObjectState = ObjectState.Modified;
            _ClassRoomService.Update(ClassRoom);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassRoomExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(ClassRoom);
        }

        // POST: odata/ClassRooms
        public async Task<IHttpActionResult> Post(ClassRoom ClassRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClassRoom.ObjectState = ObjectState.Added;
            _ClassRoomService.Insert(ClassRoom);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClassRoomExists(ClassRoom.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(ClassRoom);
        }

        //// PATCH: odata/ClassRooms(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<ClassRoom> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClassRoom ClassRoom = await _ClassRoomService.FindAsync(key);

            if (ClassRoom == null)
            {
                return NotFound();
            }

            patch.Patch(ClassRoom);
            ClassRoom.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassRoomExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(ClassRoom);
        }

        // DELETE: odata/ClassRooms(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            ClassRoom ClassRoom = await _ClassRoomService.FindAsync(key);

            if (ClassRoom == null)
            {
                return NotFound();
            }

            ClassRoom.ObjectState = ObjectState.Deleted;

            _ClassRoomService.Delete(ClassRoom);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool ClassRoomExists(Int64 key)
        {
            return _ClassRoomService.Query(e => e.Id == key).Select().Any();
        }
    }
}