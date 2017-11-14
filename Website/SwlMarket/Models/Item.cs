using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Head Talisman")]
        HeadTalisman = 116054996,
        [Display(Name = "Finger Talisman")]
        FingerTalisman = 184609570,
        [Display(Name = "Neck Talisman")]
        NeckTalisman = 116095803,
        [Display(Name = "Wrist Talisman")]
        WristTalisman = 247306948,
        [Display(Name = "Luck Talisman")]
        LuckTalisman = 246461513,
        [Display(Name = "Waist Talisman")]
        WaistTalisman = 247368388,
        [Display(Name = "Occult Talisman")]
        OccultTalisman = 191434452,
        //Upgrade
        Signet = 94364868,
        Glyph = 5128296,
        //Weapon
        Shotgun = 41012030,
        [Display(Name = "Blood Magic Focus")]
        BloodMagicFocus = 205690740,
        [Display(Name = "Fist Weapon")]
        FistWeapon = 4394078,
        [Display(Name = "Assault Rifle")]
        AssaultRifle = 91624581,
        [Display(Name = "Chaos Focus")]
        ChaosFocus = 166678788,
        [Display(Name = "Elementalism Focus")]
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
        public bool? IsExtraordinary { get; set; }
    }
}
