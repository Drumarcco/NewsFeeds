using NewsFeeds.Data.Topic;
using NewsFeeds.Entities.Topic;
using NewsFeeds.Entities.Topic.ViewModels;
using System.Collections.Generic;

namespace NewsFeeds.Data.Tests.Topic
{
    public class InMemoryTopicsRepository : ITopicsRepository
    {
        List<TopicModel> _context;

        public InMemoryTopicsRepository()
        {
            _context = new List<TopicModel>();
        }

        public void Add(TopicModel topic)
        {
            _context.Add(topic);
        }

        public List<TopicDisplayViewModel> GetTopics()
        {
            var topicsDisplays = new List<TopicDisplayViewModel>();

            foreach (var topic in _context)
            {
                topicsDisplays.Add(new TopicDisplayViewModel
                {
                    Name = topic.Name,
                    PostsCount = topic.Posts.Count,
                    SubscribersCount = topic.Subscriptions != null ? topic.Subscriptions.Count : 0
                });
            }

            return topicsDisplays;
        }

        public List<UserTopicViewModel> GetTopicsWithUserContext(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
