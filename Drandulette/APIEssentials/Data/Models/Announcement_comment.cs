using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drandulette.Controllers.Data.Models
{
    [PrimaryKey(nameof(announcment_commentID))]
    public class Announcement_comment
    {
        public string? announcment_commentID { get; set; }
        
        public string? message { get; set; }
        public string? time { get; set; }

        [NotMapped]
        public User? user { get; set; }

        [ForeignKey(nameof(mailLogin))]
        public string? mailLogin { get; set; }

        [ForeignKey(nameof(announcmentID))]
        public string? announcmentID { get; set; }
    }
}
