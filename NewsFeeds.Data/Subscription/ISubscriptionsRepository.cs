namespace NewsFeeds.Data.Subscription
{
    public interface ISubscriptionsRepository
    {
        void Subscribe(string topicName, string userId);
        void Unsubscribe(string topicName, string userId);
    }
}
