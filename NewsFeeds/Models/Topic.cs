using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsFeeds.Models
{
    public class Topic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(30, ErrorMessage = "Name's max length is 30 characters")]
        public string Name { get; set; }
    }
}
