using Microsoft.EntityFrameworkCore;
using Senswise.Context.Entity;

namespace Senswise.Context
{
    public class SenswiseContext:DbContext
    {
        protected readonly IConfiguration Configuration;

        public SenswiseContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure any additional model constraints here.
        }
        public DbSet<User> Users { get; set; }
    }
}
