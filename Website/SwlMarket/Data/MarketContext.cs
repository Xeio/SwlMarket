using Microsoft.EntityFrameworkCore;
using SwlMarket.Models;

namespace SwlMarket.Data
{
    public class MarketContext : DbContext
    {
        public MarketContext(DbContextOptions<MarketContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }
    }
}
