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
        public DbSet<HistoricalPrice> Prices { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<CurrentPrice> MostRecentPrices { get; set; }
        public DbSet<IPEntry> IPs { get; set; }
    }
}
