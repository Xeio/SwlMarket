using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SwlMarket.Models;
using SwlMarket.Data;
using Microsoft.EntityFrameworkCore;

namespace SwlMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly MarketContext _marketContext;
        public HomeController(MarketContext context)
        {
            _marketContext = context;
        }

        public async Task<IActionResult> Prices()
        {
            //Entity core framework 2.1 should support group-by better, but this may always be best as sql?
            var x = _marketContext.Prices
                .FromSql("select * from Prices where Id in (select max(Id) from Prices group by ItemId)")
                .Include(p => p.Item)
                .Where(p => ShowOnHome(p.Item))
                .OrderBy(p => p.Item.Name)
                .AsNoTracking()
                .ToListAsync();

            return View(await x);
        }

        private bool ShowOnHome(Item item)
        {
            switch (item.ItemCategory)
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
                    if (item.Rarity < Rarity.Superior) return false;
                    if (item.Name.EndsWith("Mk I")) return false;
                    return item.IsExtraordinary ?? false;
                case ItemCategory.FingerTalisman:
                case ItemCategory.HeadTalisman:
                case ItemCategory.LuckTalisman:
                case ItemCategory.NeckTalisman:
                case ItemCategory.OccultTalisman:
                case ItemCategory.WaistTalisman:
                case ItemCategory.WristTalisman:
                    if (item.Name.StartsWith("Faded ")) return false;
                    return item.IsExtraordinary ?? false;
                case ItemCategory.Glyph:
                    return item.Name.StartsWith("Intricate ");
                case ItemCategory.Gadget:
                    return item.Rarity >= Rarity.Mythic;
            }
            return true;
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
