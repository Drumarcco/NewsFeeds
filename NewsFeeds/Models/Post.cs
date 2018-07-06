using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsFeeds.Models
{
    public class Post
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

        public ApplicationUser Author { get; set; }

        [Required]
        [ForeignKey("Topic")]
        public string TopicName { get; set; }

        public Topic Topic { get; set; }
    }
}