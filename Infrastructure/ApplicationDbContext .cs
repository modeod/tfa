using Domain.JobTitle;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<JobTitle> JobTitles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobTitle>().HasKey(jt => jt.Id);

            modelBuilder.Entity<JobTitle>().HasQueryFilter(jt => !jt.IsDeleted);
        }
    }
}
