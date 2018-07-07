using Microsoft.AspNet.Identity;
using NewsFeeds.Data.Post;
using System.Web.Mvc;

namespace NewsFeeds.Web.Controllers
{
    public class NewsFeedController : Controller
    {
        IPostsRepository _postsRepository;

        public NewsFeedController(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        // GET: NewsFeed
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var posts = _postsRepository.GetUserNewsFeed(userId);
            return View(posts);
        }
    }
}