using CentralModels.Administration;
using Microsoft.EntityFrameworkCore;

namespace CentralAPIs.DBConfiguration
{
    public class AdminstrationDbContext : DbContext
    {
        public AdminstrationDbContext(DbContextOptions<AdminstrationDbContext> options) : base(options) { }

        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<UserDetailsView> UserDetailsView { get; set; }
        public DbSet<UserSession> UserSession { get; set; }
    }
}
