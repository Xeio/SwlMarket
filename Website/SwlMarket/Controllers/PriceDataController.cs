using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SwlMarket.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace SwlMarket.Controllers
{
    [Produces("application/json")]
    [Route("api/PriceData")]
    public class PriceDataController : Controller
    {
        private readonly MarketContext _marketContext;

        public PriceDataController(MarketContext context)
        {
            _marketContext = context;
        }

        [HttpGet("Item/{id}/{allTime?}")]
        public async Task<IActionResult> Item(int id, bool allTime = false)
        {
            var pricesQuery = _marketContext.Prices
                .Where(p => p.ItemID == id);
            if (!allTime)
            {
                pricesQuery = pricesQuery.Where(p => p.Time > DateTime.Now.AddMonths(-6));
            }
            var prices = await pricesQuery.OrderBy(p => p.Time)
                .AsNoTracking()
                .ToListAsync();

            var result = new
            {
                dates = prices.Select(p => p.Time.ToString("yyyy-MM-dd HH:mm")).ToList(),
                prices = prices.Select(p => p.Marks).ToList()
            };

            return Json(result);
        }
    }
}