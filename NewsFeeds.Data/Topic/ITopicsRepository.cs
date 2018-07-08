using NewsFeeds.Entities.Topic.ViewModels;
using System.Collections.Generic;

namespace NewsFeeds.Data.Topic
{
    public interface ITopicsRepository
    {
        List<TopicDisplayViewModel> GetTopics();
        List<UserTopicViewModel> GetTopicsWithUserContext(string userId);
    }
}
