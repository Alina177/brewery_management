using BreweryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryManagement.Data
{
    public class BreweryContext : DbContext
    {
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }
        public DbSet<WholesalerStock> WholesalerStocks { get; set; }

        public BreweryContext(DbContextOptions<BreweryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Breweries
            modelBuilder.Entity<Brewery>().HasData(new Brewery
            {
                Id = 1,
                Name = "Abbaye de Leffe"
            });

            // Seed Beers
            modelBuilder.Entity<Beer>().HasData(new Beer
            {
                Id = 1,
                Name = "Leffe Blonde",
                AlcoholContent = 6.6,
                Price = 2.20m,
                BreweryId = 1 // Specify FK, do not set the navigation property
            });

            // Seed Wholesalers
            modelBuilder.Entity<Wholesaler>().HasData(new Wholesaler
            {
                Id = 1,
                Name = "GeneDrinks"
            });

            // Seed Wholesaler Stocks
            modelBuilder.Entity<WholesalerStock>().HasData(new WholesalerStock
            {
                Id = 1,
                WholesalerId = 1, // FK to GeneDrinks
                BeerId = 1,       // FK to Leffe Blonde
                Quantity = 10
            });
        }
    }
}
