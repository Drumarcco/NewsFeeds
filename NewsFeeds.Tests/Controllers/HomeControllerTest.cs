using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewsFeeds.Data.Generic;
using NewsFeeds.Entities.Post;
using NewsFeeds.Entities.Subscription;
using NewsFeeds.Entities.Topic;
using NewsFeeds.Entities.Topic.ViewModels;
using NewsFeeds.Web.Controllers;
using NewsFeeds.Web.Util;
using Rhino.Mocks;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NewsFeeds.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController homeController;
        private IUnitOfWork unitOfWork;
        private ICurrentUser currentUser;
        private IRepository<TopicModel> topicRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            unitOfWork = MockRepository.GenerateMock<IUnitOfWork>();
            currentUser = MockRepository.GenerateMock<ICurrentUser>();
            topicRepository = MockRepository.GenerateMock<IRepository<TopicModel>>();
            List<TopicModel> topics = new List<TopicModel>();

            topics.Add(new TopicModel
            {
                Name = "cats",
                Posts = new List<PostModel>(),
                Subscriptions = new List<SubscriptionModel>()
            });
            topicRepository.Stub(rep => rep.Get()).Return(topics);
            unitOfWork.Stub(uow => uow.TopicRepository).Return(topicRepository);
            homeController = new HomeController(unitOfWork, currentUser);
        }

        [TestMethod]
        public void Index()
        {
            currentUser.Stub(c => c.IsLoggedIn()).Return(true);
            var result = homeController.Index();
            var redirect = result as RedirectToRouteResult;
            Assert.AreEqual("Index", redirect.RouteValues["action"]);
            Assert.AreEqual("NewsFeed", redirect.RouteValues["controller"]);
        }

        [TestMethod]
        public void IndexWithNoAuth()
        {
            currentUser.Stub(c => c.IsLoggedIn()).Return(false);
            homeController = new HomeController(unitOfWork, currentUser);
            var result = homeController.Index() as ViewResult;
            var model = result.Model as IEnumerable<TopicDisplayViewModel>;
            var topic = model.ElementAt(0);
            Assert.AreEqual("cats", topic.Name);
            Assert.AreEqual(0, topic.SubscriptionsCount);
            Assert.AreEqual(0, topic.Posts.Count);
        }
    }
}
