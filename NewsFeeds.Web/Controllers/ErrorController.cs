using System.Web.Mvc;

namespace NewsFeeds.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NotFound()
        {
            return View();
        }
    }
}