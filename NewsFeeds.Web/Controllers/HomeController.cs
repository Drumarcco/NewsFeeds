using NewsFeeds.Data.Generic;
using NewsFeeds.Entities.Topic.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace NewsFeeds.Web.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork uow;

        public HomeController(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }

        public ActionResult Index()
        {
            var topics = uow.TopicRepository.Get().ToList();

            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "NewsFeed");
            }
            else
            {
                return View(topics.Select(t => TopicMapper.Map(t)));
            }
        }
    }
}