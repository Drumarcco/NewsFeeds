using Microsoft.AspNet.Identity;
using NewsFeeds.Data.Generic;
using NewsFeeds.Entities.Post.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace NewsFeeds.Web.Controllers
{
    public class NewsFeedController : Controller
    {
        IUnitOfWork uow;

        public NewsFeedController(IUnitOfWork unitOfWork)
        {
            this.uow = unitOfWork;
        }

        // GET: NewsFeed
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var posts = uow.PostRepository
                .Get(
                    p => p.Topic.Subscriptions.Any(s => s.UserId == userId),
                    includeProperties: "Author",
                    orderBy: p => p.OrderByDescending(post => post.PostedAt)
                )
                .Select(p => PostMapper.Map(p))
                .ToList();

            return View(posts);
        }
    }
}