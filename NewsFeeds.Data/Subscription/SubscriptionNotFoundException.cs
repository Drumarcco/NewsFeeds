using NewsFeeds.Data.Exceptions;

namespace NewsFeeds.Data.Subscription
{
    public class SubscriptionNotFoundException : NotFoundException
    {
        public SubscriptionNotFoundException(string userId, string topicName) : base(string.Format("Subscription with userId ${0} and topic ${1} was not found", userId, topicName))
        {

        }
    }
}
