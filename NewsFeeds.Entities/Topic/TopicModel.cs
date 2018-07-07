using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsFeeds.Entities.Topic
{
    [Table("Topics")]
    public class TopicModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(30, ErrorMessage = "Name's max length is 30 characters")]
        public string Name { get; set; }
    }
}