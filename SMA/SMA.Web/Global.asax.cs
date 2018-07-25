using SMA.Entities.Models;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MySql.Data.Entity;

namespace SMA.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer<SMAContext>(new SMADBInitializer());
            //DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(ODataConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        //protected void Application_BeginRequest(object sender, System.EventArgs e)
        //{
        //    System.Web.HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        //    if (System.Web.HttpContext.Current.Request.HttpMethod == "OPTIONS")
        //    {
        //        System.Web.HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
        //        System.Web.HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
        //        System.Web.HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
        //        System.Web.HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
        //        System.Web.HttpContext.Current.Response.End();
        //    }
        //}
    }
}
