using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SwlMarket.Models;
using SwlMarket.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace SwlMarket.Controllers
{
    public class HomeController : Controller
    {
        const int MAX_SEARCH_RESULTS = 60;

        private readonly MarketContext _marketContext;
        public HomeController(MarketContext context)
        {
            _marketContext = context;
        }

        public IActionResult Prices()
        {
            var prices = _marketContext.MostRecentPrices
                .Include(p => p.Item)
                .OrderByDescending(p => p.Item.Rarity)
                .ThenBy(p => p.Item.Name)
                .AsNoTracking()
                .Where(ShowOnHome);

            return View(prices);
        }

        private bool ShowOnHome(Price price)
        {
            if (price.Time.AddDays(7) < DateTime.UtcNow) return false;
            switch (price.Item.ItemCategory)
            {
                case ItemCategory.AssaultRifle:
                case ItemCategory.Blade:
                case ItemCategory.BloodMagicFocus:
                case ItemCategory.ChaosFocus:
                case ItemCategory.ElementalismFocus:
                case ItemCategory.FistWeapon:
                case ItemCategory.Hammer:
                case ItemCategory.Pistols:
                case ItemCategory.Shotgun:
                    if (price.Item.Rarity < Rarity.Superior) return false;
                    if (price.Marks < 10000) return false;
                    return price.Item.IsExtraordinary ?? false;
                case ItemCategory.FingerTalisman:
                case ItemCategory.HeadTalisman:
                case ItemCategory.LuckTalisman:
                case ItemCategory.NeckTalisman:
                case ItemCategory.OccultTalisman:
                case ItemCategory.WaistTalisman:
                case ItemCategory.WristTalisman:
                    if (price.Marks < 2000) return false;
                    if (!price.Item.Name.StartsWith("Radiant ")) return false;
                    return price.Item.IsExtraordinary ?? false;
                case ItemCategory.Glyph:
                    return price.Item.Name.StartsWith("Intricate ");
                case ItemCategory.Gadget:
                    return price.Item.Rarity >= Rarity.Legendary;
                case ItemCategory.Museum:
                    return price.Marks > 1000;
                case ItemCategory.Clothing:
                    return price.Item.Rarity >= Rarity.Epic;
            }
            return true;
        }

        [HttpGet("/Home/Item/{id}/{allTime?}")]
        public async Task<IActionResult> Item(int id, bool allTime = false)
        {
            var item = await _marketContext.Items.SingleOrDefaultAsync(i => i.ID == id);
            
            if(item == null)
            {
                return NotFound();
            }

            if (allTime)
            {
                ViewData["AllTime"] = true;
            }

            return View(item);
        }
        
        public async Task<IActionResult> Search([FromQuery]string name)
        {
            var prices = await _marketContext.MostRecentPrices
                .Include(p => p.Item)
                .Where(p => p.Item.Name.Contains(name))
                .OrderByDescending(p => p.Item.Rarity)
                .ThenBy(p => p.Item.Name)
                .Take(MAX_SEARCH_RESULTS)
                .AsNoTracking()
                .ToListAsync();

            ViewData["HasMaxItems"] = prices.Count == MAX_SEARCH_RESULTS;
            ViewData["MaxItemCount"] = MAX_SEARCH_RESULTS;

            return View(prices);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
