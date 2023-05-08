using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drandulette.Controllers.Data.Models
{
    [PrimaryKey(nameof(topic_commentID))]
    public class Topic_comment
    {
        public string? topic_commentID { get; set; }
        public string? message { get; set; }
        public string? time { get; set; }

        [ForeignKey(nameof(mailLogin))]
        public string? mailLogin { get; set; }

        [ForeignKey(nameof(topicID))]
        public string? topicID { get; set; }

        [NotMapped]
        public User? user { get; set; }
    }
}
