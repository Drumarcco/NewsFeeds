using NewsFeeds.Data.Exceptions;

namespace NewsFeeds.Data.Topic
{
    public class TopicNotFoundException : NotFoundException
    {
        public TopicNotFoundException(string topicName) : base(string.Format("Topic {0} not found", topicName))
        {

        }
    }
}
