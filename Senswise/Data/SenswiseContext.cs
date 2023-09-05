using Microsoft.EntityFrameworkCore;
using Senswise.Data.Entity;
using Senswise.Data.Entity;

namespace Senswise.Data
{
    public class SenswiseContext:DbContext
    {
     
        public SenswiseContext(DbContextOptions<SenswiseContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
