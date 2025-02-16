using DDD_Project.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DDD_Project.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Phone> Phones { get; set; }
        public DbSet<Country> Countries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Country>().Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
