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
    public class ImageController : ODataController
    {
        private readonly IImageService _ImageService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ImageController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IImageService ImageService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ImageService = ImageService;
        }

        // GET: odata/Images
        [HttpGet]
        [Queryable]
        public IQueryable<Image> GetImage()
        {
            
            var l= _ImageService.Queryable().ToList();
            return _ImageService.Queryable();
        }

        // GET: odata/Images(5)
        [Queryable]
        public SingleResult<Image> GetImage([FromODataUri] Int64 key)
        {
            return SingleResult.Create(_ImageService.Queryable().Where(t => t.Id == key));
        }

        // PUT: odata/Images(5)
        public async Task<IHttpActionResult> Put(Int64 key, Image Image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Image.Id)
            {
                return BadRequest();
            }

            Image.ObjectState = ObjectState.Modified;
            _ImageService.Update(Image);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Image);
        }

        // POST: odata/Images
        public async Task<IHttpActionResult> Post(Image Image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Image.ObjectState = ObjectState.Added;
            _ImageService.Insert(Image);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImageExists(Image.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(Image);
        }

        //// PATCH: odata/Images(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Int64 key, Delta<Image> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Image Image = await _ImageService.FindAsync(key);

            if (Image == null)
            {
                return NotFound();
            }

            patch.Patch(Image);
            Image.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(Image);
        }

        // DELETE: odata/Images(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Image Image = await _ImageService.FindAsync(key);

            if (Image == null)
            {
                return NotFound();
            }

            Image.ObjectState = ObjectState.Deleted;

            _ImageService.Delete(Image);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        private bool ImageExists(Int64 key)
        {
            return _ImageService.Query(e => e.Id == key).Select().Any();
        }
    }
}