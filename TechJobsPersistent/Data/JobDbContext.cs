using TechJobsPersistent.Models;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Data
{
    public class JobDbContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }

        public JobDbContext(DbContextOptions<JobDbContext> options)
            : base(options)
        {
        }
    }
}
