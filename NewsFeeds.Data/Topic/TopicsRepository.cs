using NewsFeeds.Data.Context;
using NewsFeeds.Entities.Topic;
using NewsFeeds.Entities.Topic.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NewsFeeds.Data.Topic
{
    public class TopicsRepository : ITopicsRepository
    {
        public List<TopicDisplayViewModel> GetTopics()
        {
            using (var context = new ApplicationDbContext())
            {
                List<TopicModel> topics = new List<TopicModel>();
                topics = context.Topics
                    .Include(t => t.Subscriptions)
                    .Include(t => t.Posts)
                    .ToList();

                if (topics != null)
                {
                    List<TopicDisplayViewModel> topicsDisplay = new List<TopicDisplayViewModel>();

                    foreach (var topic in topics)
                    {
                        var topicDisplay = new TopicDisplayViewModel
                        {
                            Name = topic.Name,
                            SubscribersCount = topic.Subscriptions.Count,
                            PostsCount = topic.Posts.Count
                        };
                        topicsDisplay.Add(topicDisplay);
                    }

                    return topicsDisplay;
                }

                return null;
            }
        }
    }
}
