using NewsFeeds.Entities.Topic.ViewModels;
using System.Collections.Generic;

namespace NewsFeeds.Data.Topic
{
    public interface ITopicsRepository
    {
        List<TopicDisplayViewModel> GetTopics();
        List<UserTopicViewModel> GetTopicsWithUserContext(string userId);
        TopicDisplayViewModel GetTopic(string topicName, string queryPosts);
        UserTopicViewModel GetTopicWithUserContext(string userId, string topicName, string queryPosts);
    }
}
