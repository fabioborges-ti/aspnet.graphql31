using Microsoft.EntityFrameworkCore;
using QuoteOfTheDay.Data.Extensions;
using QuoteOfTheDay.Entities;

namespace QuoteOfTheDay.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Category>()
                .HasMany(c => c.Quotes)
                .WithOne(q => q.Category)
                .IsRequired();

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
