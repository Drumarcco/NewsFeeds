using NewsFeeds.Entities.Post;
using NewsFeeds.Entities.Post.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace NewsFeeds.Entities.Topic.ViewModels
{
    public class TopicMapper
    {
        static public UserTopicViewModel Map(TopicModel topic, string userId, ICollection<PostModel> posts)
        {


            return new UserTopicViewModel
            {
                IsSubscribed = topic.Subscriptions.Any(s => s.UserId == userId),
                Name = topic.Name,
                Posts = posts.Select(p => PostMapper.Map(p)).ToList(),
                SubscriptionsCount = topic.Subscriptions.Count
            };
        }

        static public TopicDisplayViewModel Map(TopicModel topic)
        {
            return new TopicDisplayViewModel
            {
                Name = topic.Name,
                Posts = topic.Posts.OrderByDescending(p => p.PostedAt).Select(p => PostMapper.Map(p)).ToList(),
                SubscriptionsCount = topic.Subscriptions.Count
            };
        }
    }
}
