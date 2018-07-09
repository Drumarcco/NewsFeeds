using Microsoft.AspNet.Identity;
using NewsFeeds.Data.Exceptions;
using NewsFeeds.Data.Generic;
using NewsFeeds.Data.Topic;
using NewsFeeds.Entities.Post;
using NewsFeeds.Entities.Topic.ViewModels;
using NewsFeeds.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace NewsFeeds.Web.Controllers
{
    [RoutePrefix("Topic")]
    public class TopicController : Controller
    {
        private IUnitOfWork uow;
        private ICurrentUser currentUser;

        public TopicController(IUnitOfWork uow, ICurrentUser currentUser)
        {
            this.uow = uow;
            this.currentUser = currentUser;
        }

        // GET: Topic
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            IEnumerable<UserTopicViewModel> topics = uow.TopicRepository
                .Get(null, null, "Subscriptions,Posts,Posts.Author")
                .ToList()
                .Select(t => TopicMapper.Map(t, userId, t.Posts));

            return View(topics.ToList());
        }

        [Route("{name}")]
        [HandleError(ExceptionType = typeof(TopicNotFoundException), View = "NotFound")]
        public ActionResult Details(string name, string query)
        {

            if (query == null)
            {
                query = "";
            }

            query = query.ToLower();

            var topic = uow.TopicRepository
                .Get(
                    filter: t => t.Name.ToLower() == name,
                    includeProperties: "Subscriptions,Posts.Author"
                )
                .FirstOrDefault();


            if (topic == null)
            {
                throw new TopicNotFoundException(name);
            }

            Expression<Func<PostModel, bool>> postsFilter = p =>
                p.TopicName.ToLower() == name.ToLower() &&
                (p.Title.ToLower().Contains(query)
                || p.Content.ToLower().Contains(query)
                || p.Author.UserName.ToLower().Contains(query)
                || p.TopicName.ToLower().Contains(query));

            if (currentUser.IsLoggedIn())
            {
                var userId = User.Identity.GetUserId();
                var user = uow.UserRepository.GetByID(userId);

                var posts = uow.PostRepository.Get(postsFilter, p => p.OrderByDescending(post => post.PostedAt), "Author");


                return View(TopicMapper.Map(topic, userId, posts.ToList()));
            }


            return View(TopicMapper.Map(new Entities.Topic.TopicModel
            {
                Posts = uow.PostRepository.Get(postsFilter).Where(p => p.TopicName == name).ToList(),
                Name = name,
                Subscriptions = topic.Subscriptions
            }));
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
                uow.SubscriptionRepository.Insert(new Entities.Subscription.SubscriptionModel
                {
                    TopicName = name,
                    UserId = userId
                });

                uow.Save();

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
                var topic = uow.TopicRepository.GetByID(name);

                uow.SubscriptionRepository.Delete(new Entities.Subscription.SubscriptionModel
                {
                    UserId = userId,
                    TopicName = name
                });

                uow.Save();

                return new HttpStatusCodeResult(200);
            }
            catch (NotFoundException notFoundException)
            {
                return new HttpNotFoundResult(notFoundException.Message);
            }
        }
    }
}