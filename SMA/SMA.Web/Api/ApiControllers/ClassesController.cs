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
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Cors;
namespace SMA.Web.Api
{
    [EnableCors("*", "*", "*")]
    public class ClassesController : ApiController
    {
        private readonly IClassService _ClassService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ClassesController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IClassService ClassService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ClassService = ClassService;
        }

        // Get All Classs
         [Route("api/Classes")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Class> Classs = _ClassService.Queryable().ToList();
                return JsonConvert.JsonConvertListResult(Request, Classs);
            }
            catch (Exception ex)
            {
                return JsonConvert.JsonErrorResult(Request, ex.Message);
            }
        }

        // Get Class by Id
         [Route("api/Classes/{id}")]
         public HttpResponseMessage Get(int id)
         {
             try
             {
                 Class Class = _ClassService.Find(id);
                 return JsonConvert.JsonConvertObjectResult(Request, Class);
             }
             catch (Exception ex)
             {
                 return JsonConvert.JsonErrorResult(Request, ex.Message);
             }
         }

        ////Get Class by Libel
        //[Route("api/Classes/{name:alpha}")]
        //public IEnumerable<Class> Get(string name)
        //{
        //    return _ClassService.GetClassesByName(name).ToList();
        //    //      return (from p in cc.Set<Student>()
        //    //      where p.FirstName == name
        //    //      select new { FirstName = p.FirstName,LastName = p.LastName,Email =p.Email }).ToList()
        //    //.Select(x => new Student { FirstName = x.FirstName,LastName = x.LastName,Email =x.Email});


        //}


        // Insert Class
        [Route("api/Classes/ajout")]
        public async Task<HttpResponseMessage> Post([FromBody]Class value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ClassService.Insert(value);
                    var response = Request.CreateResponse<Class>(HttpStatusCode.Created, value);
                    await _unitOfWorkAsync.SaveChangesAsync();
                    return response;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Model state is invalid");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Update Class
        [Route("api/Classes/modifier/{id}")]
        public async Task<HttpResponseMessage> Put(int id, Class value)
        {
            try
            {
                value.Id = id;
                _ClassService.Update(value);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        //public HttpResponseMessage Put(string Nom, Class value)
        //{
        //    try
        //    {
        //        _ClassService.Update(value);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        // Delete Class
        [Route("api/Classes/supprimer/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                _ClassService.Delete(id);
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}