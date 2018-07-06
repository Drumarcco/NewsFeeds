﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsFeeds.Models
{
    public class Subscription
    {
        [Required]
        [Column(Order = 0), Key, ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        [Column(Order = 1), Key, ForeignKey("Topic")]
        public string TopicName { get; set; }

        [Required]
        public Topic Topic { get; set; }
    }
}