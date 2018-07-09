using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewsFeeds.Data.Tests.Topic;
using NewsFeeds.Data.Topic;
using NewsFeeds.Entities.Post;
using NewsFeeds.Entities.Subscription;
using NewsFeeds.Entities.Topic;
using NewsFeeds.Entities.Topic.ViewModels;
using NewsFeeds.Web.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NewsFeeds.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private ITopicsRepository topicsRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            topicsRepository = new InMemoryTopicsRepository();
            var inMemoryTopicsRepository = (InMemoryTopicsRepository)topicsRepository;
            var dogPostList = new List<PostModel>();
            dogPostList.Add(new PostModel { });
            dogPostList.Add(new PostModel { });
            dogPostList.Add(new PostModel { });
            var dogSubscriptions = new List<SubscriptionModel>();
            dogSubscriptions.Add(new SubscriptionModel { });


            inMemoryTopicsRepository.Add(new TopicModel
            {
                Name = "dogs",
                Posts = dogPostList,
                Subscriptions = dogSubscriptions

            });

            inMemoryTopicsRepository.Add(new TopicModel
            {
                Name = "cats",
                Posts = new List<PostModel>()
            });
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(topicsRepository);

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var model = result.ViewData.Model as List<TopicDisplayViewModel>;
            var cat = model.Find(m => m.Name == "cats");
            var dog = model.Find(m => m.Name == "dogs");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(cat);
            Assert.AreEqual(dog.PostsCount, 3);
            Assert.AreEqual(cat.PostsCount, 0);
            Assert.AreEqual(dog.SubscribersCount, 1);
            Assert.AreEqual(cat.SubscribersCount, 0);
            Assert.IsNotNull(dog);
            CollectionAssert.AllItemsAreUnique(model);

        }
    }
}
