using NewsFeeds.Entities.ApplicationUser;
using NewsFeeds.Entities.Topic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsFeeds.Entities.Post
{
    [Table("Posts")]
    public class PostModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime PostedAt { get; set; }

        [Required]
        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        public ApplicationUserModel Author { get; set; }

        [Required]
        [ForeignKey("Topic")]
        public string TopicName { get; set; }

        public TopicModel Topic { get; set; }
    }
}