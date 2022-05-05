using FengShuiNumber.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FengShuiNumber.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected internal virtual void OnConfiguring(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhoneNumber>().HasKey(x=>x.Id);
        }
    }
}
