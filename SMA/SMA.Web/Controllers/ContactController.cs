using System.Web.Mvc;

namespace SMA.Web.Controllers
{
    public class ContactController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Edit()
        {
            return PartialView();
        }
    }
}