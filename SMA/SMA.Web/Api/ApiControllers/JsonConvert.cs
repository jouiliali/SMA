using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;
using System.Text;

namespace SMA.Web.Api
{
    public class JsonConvert
    {
        private static JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();

        public static HttpResponseMessage JsonConvertListResult<T>(HttpRequestMessage Request, IEnumerable<T> listData)
        {
            var json = @"{""status"": ""SUCCESS"",""data"":" + jsonSerialiser.Serialize(listData) + "}";
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        public static HttpResponseMessage JsonConvertObjectResult<T>(HttpRequestMessage Request, T objData)
        {
            var json = @"{""status"": ""SUCCESS"",""data"":" + jsonSerialiser.Serialize(objData) + "}";
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        public static HttpResponseMessage JsonErrorResult(HttpRequestMessage Request, String strErrorMessage)
        {
            var json = @"{""status"": ""ERROR"",""data"":{ Message : " + strErrorMessage+"}}";
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }
    }
}