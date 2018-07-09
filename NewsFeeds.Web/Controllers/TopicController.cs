using Microsoft.AspNet.Identity;
using NewsFeeds.Data.Exceptions;
using NewsFeeds.Data.Subscription;
using NewsFeeds.Data.Topic;
using NewsFeeds.Entities.Topic.ViewModels;
using System.Web.Mvc;

namespace NewsFeeds.Web.Controllers
{
    [RoutePrefix("Topic")]
    public class TopicController : Controller
    {
        ITopicsRepository _topicsRepository;
        ISubscriptionsRepository _subscriptionRepository
;
        public TopicController(ITopicsRepository topicsRepository, ISubscriptionsRepository subscriptionRepository)
        {
            _topicsRepository = topicsRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        // GET: Topic
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var topicsWithUserContext = _topicsRepository.GetTopicsWithUserContext(userId);

            return View(topicsWithUserContext);
        }

        [Route("{name}")]
        [HandleError(ExceptionType = typeof(TopicNotFoundException), View = "NotFound")]
        public ActionResult Details(string name)
        {
            TopicDisplayViewModel topic;


            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                topic = _topicsRepository.GetTopicWithUserContext(userId, name);
            }
            else
            {
                topic = _topicsRepository.GetTopic(name);
            }

            return View(topic);
        }

        // POST: Topic/{name}/subscription
        [Authorize]
        [HttpPost]
        [Route("{name}/subscription")]
        public HttpStatusCodeResult Subscribe(string name)
        {
            var userId = User.Identity.GetUserId();
            try
            {
                _subscriptionRepository.Subscribe(name, userId);
                return new HttpStatusCodeResult(200);
            }
            catch (NotFoundException notFoundException)
            {
                return new HttpNotFoundResult(notFoundException.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("{name}/subscription")]
        public HttpStatusCodeResult Unsubscribe(string name)
        {
            var userId = User.Identity.GetUserId();
            try
            {
                _subscriptionRepository.Unsubscribe(name, userId);
                return new HttpStatusCodeResult(200);
            }
            catch (NotFoundException notFoundException)
            {
                return new HttpNotFoundResult(notFoundException.Message);
            }
        }
    }
}