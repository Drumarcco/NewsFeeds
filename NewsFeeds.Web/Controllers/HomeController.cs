using NewsFeeds.Data.Generic;
using NewsFeeds.Entities.Topic.ViewModels;
using NewsFeeds.Web.Util;
using System.Linq;
using System.Web.Mvc;

namespace NewsFeeds.Web.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork uow;
        ICurrentUser currentUser;

        public HomeController(IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            uow = unitOfWork;
            this.currentUser = currentUser;
        }

        public ActionResult Index()
        {
            var topics = uow.TopicRepository
                .Get(includeProperties: "Posts,Posts.Author,Subscriptions")
                .ToList();

            if (currentUser.IsLoggedIn())
            {
                return RedirectToAction("Index", "NewsFeed");
            }
            else
            {
                return View(topics.Select(t => TopicMapper.Map(t)).ToList());
            }
        }
    }
}