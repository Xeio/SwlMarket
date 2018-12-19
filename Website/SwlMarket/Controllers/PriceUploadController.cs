using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwlMarket.Data;
using SwlMarket.Models;

namespace SwlMarket.Controllers
{
    [Produces("application/json")]
    [Route("api/PriceUpload")]
    public class PriceUploadController : Controller
    {
        private readonly MarketContext _marketContext;

        public PriceUploadController(MarketContext context)
        {
            _marketContext = context;
        }

        [HttpGet]
        public async Task<bool> Get([FromQuery]string name,
            [FromQuery]int? price,
            [FromQuery]Rarity? rarity,
            [FromQuery]ItemCategory? category,
            [FromQuery(Name = "ext")]bool? extraordinary)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            if (price == null) return false;
            if (rarity == null) return false;
            if (category == null) return false;
            if (extraordinary == null) return false;
            
            var ipEntry = await _marketContext.IPs.SingleOrDefaultAsync(k => k.IP == HttpContext.Connection.RemoteIpAddress.ToString());
            if (ipEntry == null)
            {
                ipEntry = new IPEntry() { IP = HttpContext.Connection.RemoteIpAddress.ToString() };
            }
            if (ipEntry.Blocked)
            {
                return false;
            }

            var item = await _marketContext.Items.SingleOrDefaultAsync(i => i.Name == name && i.Rarity == rarity);
            if(item == null)
            {
                item = new Item() { Name = name, Rarity = rarity, ItemCategory = category, IsExtraordinary = extraordinary };
            }
            else
            {
                var mostRecentPrice = await _marketContext.MostRecentPrices.SingleAsync(mrp => mrp.ItemID == item.ID);
                if (mostRecentPrice.Marks == price && mostRecentPrice.Time.AddHours(3) > DateTime.UtcNow)
                {
                    //Ignore duplicated prices if they happen over a small time window
                    return false;
                }
            }

            var newPrice = new HistoricalPrice() { Item = item, Marks = price.Value, Time = DateTime.UtcNow, IP = ipEntry };
            _marketContext.Prices.Add(newPrice);

            await _marketContext.SaveChangesAsync();

            return true;
        }
    }
}