using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace bARTSolutionsTest.Models.AppContext
{
    public class AppContext : DbContext
    {
        public DbSet<Account> accounts { get; set; }
        public DbSet<Incident> incidents { get; set; }
        public DbSet<Contact> contacts { get; set; }

        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            builder.Entity<Contact>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            //builder.Entity<Incident>()
            //.Property(p => p.Name)
            //.HasDefaultValueSql("NEWID()");

        }
    }
}
