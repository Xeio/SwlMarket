using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwlMarket.Models
{
    public enum ItemCategory
    {
        //Cosmetic
        Pet = 22212,
        Clothing = 57339111,
        Sprint = 5532756,
        //Misc
        Consumable = 180871029,
        Essence = 197642084,
        Gadget = 226155763,
        Museum = 88906941,
        //Talisman
        HeadTalisman = 116054996,
        FingerTalisman = 184609570,
        NeckTalisman = 116095803,
        WristTalisman = 247306948,
        LuckTalisman = 246461513,
        WaistTalisman = 247368388,
        OccultTalisman = 191434452,
        //Upgrade
        Signet = 94364868,
        Glyph = 5128296,
        //Weapon
        Shotgun = 41012030,
        BloodMagicFocus = 205690740,
        FistWeapon = 4394078,
        AssaultRifle = 91624581,
        ChaosFocus = 166678788,
        ElementalismFocus = 71672516,
        Pistols = 244175283,
        Hammer = 157308306,
        Blade = 93626565,
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
