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
                .OrderBy(p => p.Item.Name)
                .AsNoTracking()
                .ToListAsync();

            //var y = await _marketContext.Prices.Where(p => _marketContext.Items.Any(i => i.ID == p.ItemID)).ToListAsync();

            return View(await x);
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
