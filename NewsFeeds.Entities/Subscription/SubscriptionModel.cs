using NewsFeeds.Entities.ApplicationUser;
using NewsFeeds.Entities.Topic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsFeeds.Entities.Subscription
{
    [Table("Subscriptions")]
    public class SubscriptionModel
    {
        [Required]
        [Column(Order = 0), Key, ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUserModel User { get; set; }

        [Required]
        [Column(Order = 1), Key, ForeignKey("Topic")]
        public string TopicName { get; set; }

        public TopicModel Topic { get; set; }
    }
}