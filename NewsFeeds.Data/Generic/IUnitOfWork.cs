using NewsFeeds.Entities.ApplicationUser;
using NewsFeeds.Entities.Post;
using NewsFeeds.Entities.Subscription;
using NewsFeeds.Entities.Topic;

namespace NewsFeeds.Data.Generic
{
    public interface IUnitOfWork
    {
        Repository<TopicModel> TopicRepository { get; }
        Repository<SubscriptionModel> SubscriptionRepository { get; }
        Repository<ApplicationUserModel> UserRepository { get; }
        Repository<PostModel> PostRepository { get; }
        void Save();
    }
}
