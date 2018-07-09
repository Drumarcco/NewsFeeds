using NewsFeeds.Entities.Post.ViewModels;
using System.Collections.Generic;

namespace NewsFeeds.Entities.Topic.ViewModels
{
    public class TopicDisplayViewModel
    {
        public string Name { get; set; }
        public int SubscriptionsCount { get; set; }
        public List<PostDisplayViewModel> Posts { get; set; }
    }
}
