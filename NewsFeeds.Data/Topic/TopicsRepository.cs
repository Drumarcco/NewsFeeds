using NewsFeeds.Data.Context;
using NewsFeeds.Entities.Topic;
using NewsFeeds.Entities.Topic.ViewModels;
using System.Collections.Generic;
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
                topics = context.Topics.ToList();

                if (topics != null)
                {
                    List<TopicDisplayViewModel> topicsDisplay = new List<TopicDisplayViewModel>();

                    foreach (var topic in topics)
                    {
                        var topicDisplay = new TopicDisplayViewModel
                        {
                            Name = topic.Name
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
