using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drandulette.Controllers.Data.Models
{
    [PrimaryKey(nameof(announcementID))]
    public class Announcement
    {

        public string? announcementID { get; set; }
        public int price { get; set; }
        public string? brand { get; set; }
        public string? model { get; set; }
        public int year { get; set; }
        public int mileage { get; set; }
        public string? transmission { get; set; }
        public int hp { get; set; }
        public double volume { get; set; }
        public string? fuelType { get; set; }
        public string? body { get; set; }
        public string? wheelDrive { get; set; }
        public string? picsPath { get; set; }
        public string? sellersComment { get; set; }

        [ForeignKey(nameof(mailLogin))]
        public string? mailLogin { get; set; }

        [NotMapped]
        public User? user { get; set; }

        [NotMapped]
        public List<string> pics { get; set; }
    }
}
