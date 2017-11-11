using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwlMarket.Models
{
    public enum ItemCategory
    {
        Signet = 7653089,
        Glyph = 7653088,
        Weapon = 9252860,
    }

    public enum Rarity
    {
        Worn = 1,
        Standard = 2,
        Superior = 3,
        Epic = 4,
        Mythic = 5,
        Legendary = 6,
    }

    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ItemCategory? ItemCategory { get; set; }
        public Rarity? Rarity { get; set; }
    }
}
