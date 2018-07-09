using NewsFeeds.Entities.ApplicationUser;
using NewsFeeds.Entities.Post;
using NewsFeeds.Entities.Subscription;
using NewsFeeds.Entities.Topic;

namespace NewsFeeds.Data.Generic
{
    public interface IUnitOfWork
    {
        IRepository<TopicModel> TopicRepository { get; }
        IRepository<SubscriptionModel> SubscriptionRepository { get; }
        IRepository<ApplicationUserModel> UserRepository { get; }
        IRepository<PostModel> PostRepository { get; }
        void Save();
    }
}
