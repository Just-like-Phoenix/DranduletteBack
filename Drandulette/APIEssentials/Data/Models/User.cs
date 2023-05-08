using Microsoft.EntityFrameworkCore;

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
    }
}
