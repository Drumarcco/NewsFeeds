using NewsFeeds.Data.Context;
using NewsFeeds.Data.Topic;
using NewsFeeds.Data.User;
using NewsFeeds.Entities.Subscription;
using System.Linq;

namespace NewsFeeds.Data.Subscription
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        public void Subscribe(string topicName, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var subscriptionExists = context.Subscriptions.Any(s => s.TopicName == topicName && s.UserId == userId);

                if (subscriptionExists)
                {
                    return;
                }

                var topic = context.Topics.Where(t => t.Name == topicName).First();
                var user = context.Users.Where(u => u.Id == userId);


                if (topic == null)
                {
                    throw new TopicNotFoundException(topicName);
                }

                if (user == null)
                {
                    throw new UserNotFoundException(userId);
                }

                context.Subscriptions.Add(new SubscriptionModel
                {
                    TopicName = topicName,
                    UserId = userId
                });

                context.SaveChanges();
            }
        }

        public void Unsubscribe(string topicName, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var subscription = context.Subscriptions.First(s => s.TopicName == topicName && s.UserId == userId);

                if (subscription == null)
                {
                    throw new SubscriptionNotFoundException(userId, topicName);
                }

                context.Subscriptions.Remove(subscription);
                context.SaveChanges();
            }
        }
    }
}
