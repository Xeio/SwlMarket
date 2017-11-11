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
            [FromQuery]int? expiresIn, 
            [FromQuery]string apiKey,
            [FromQuery]bool? extraordinary)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            if (price == null) return false;
            if (rarity == null) return false;
            if (expiresIn == null || expiresIn < 1 || expiresIn > 24 * 60 * 60 * 7) return false; //Sanity check expire date to 0-7 days (really can't be more than 3 in the current SWL)
            if (string.IsNullOrWhiteSpace(apiKey)) return false;

            //TODO: Maybe make this stronger auth (crypt/salt), but this is pretty non-critical so a long key should be adequate
            var key = await _marketContext.ApiKeys.SingleOrDefaultAsync(k => k.Active && k.Key == apiKey);
            if (key == null) return false;

            var item = await _marketContext.Items.SingleOrDefaultAsync(i => i.Name == name && i.Rarity == rarity);
            if(item == null)
            {
                item = new Item() { Name = name, Rarity = rarity, ItemCategory = category };
            }
            if(item.ItemCategory == null)
            {
                item.ItemCategory = category;
            }
            if(item.IsExtraordinary == null)
            {
                item.IsExtraordinary = extraordinary;
            }

            var newPrice = new Price() { Item = item, Marks = price.Value, Time = DateTime.Now, ExpiresIn = expiresIn.Value, ApiKey = key };
            _marketContext.Prices.Add(newPrice);

            await _marketContext.SaveChangesAsync();

            return true;
        }
    }
}