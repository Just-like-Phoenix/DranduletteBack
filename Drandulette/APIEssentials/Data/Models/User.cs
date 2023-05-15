using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drandulette.Controllers.Data.Models
{
    [PrimaryKey(nameof(mailLogin))]
    public class User
    {
        public string? mailLogin { get; set; }
        public string? password { get; set; }
        public string? name { get; set; }
        public string? phone { get; set; }
        public int moderator { get; set; }
        public int verificated { get; set; }
        public int banned { get; set; }

        [NotMapped]
        public string? profilePic { get; set; }
    }
}
