using CustomJWTAuthentication.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomJWTAuthentication.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<User2> Users2 { get; set; }

        private readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            options.UseNpgsql(Configuration.GetConnectionString("PSQLConnectionString"));
        }
    }
}
