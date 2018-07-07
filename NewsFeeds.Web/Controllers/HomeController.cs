using NewsFeeds.Data.Topic;
using System.Web.Mvc;

namespace NewsFeeds.Web.Controllers
{
    public class HomeController : Controller
    {
        ITopicsRepository _topicsRepository;

        public HomeController(ITopicsRepository topicsRepository)
        {
            _topicsRepository = topicsRepository;
        }

        public ActionResult Index()
        {
            var topics = _topicsRepository.GetTopics();
            return View(topics);
        }
    }
}