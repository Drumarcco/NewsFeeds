using NewsFeeds.Models;
using NewsFeeds.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace NewsFeeds.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var topics = _context.Topics.ToList();
                HomeIndexViewModel vm = new HomeIndexViewModel { Topics = topics };
                return View(vm);
            }

            return View();
        }
    }
}