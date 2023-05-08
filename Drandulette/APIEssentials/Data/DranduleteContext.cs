using Drandulette.Controllers.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Drandulette.Controllers.Data
{
    public class DranduletteContext : DbContext
    {
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Topic> Topic { get; set; } = null!;
        public DbSet<Topic_comment> Topic_comment { get; set; } = null!;
        public DbSet<Announcement> Announcement { get; set; } = null!;
        public DbSet<Announcement_comment> Announcement_comment { get; set; } = null!;

        public DranduletteContext(DbContextOptions<DranduletteContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
