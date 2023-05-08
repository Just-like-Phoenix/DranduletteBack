using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drandulette.Controllers.Data.Models
{
    [PrimaryKey(nameof(topicID))]
    public class Topic
    {
        public string? topicID { get; set; }
        public string? topic_theme { get; set; }
        public string? topic_text { get; set; }
        public string? time { get; set; }

        [ForeignKey(nameof(mailLogin))]
        public string? mailLogin { get; set; }

        [NotMapped]
        public User? user { get; set; }
    }
}
