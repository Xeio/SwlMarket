import com.GameInterface.Tradepost;

class com.xeio.SwlMarket.AutoSearch
{
    var m_index : Number
    var m_searchCount : Number = 0;
    static var m_searches : Array = [
        //Type, subtype, search string, rarity
        //misc
        [109918787, 226155763, "", -1], //Consumable
        [109918787, 226155763, "Key ", -1],
        [109918787, 226155763, "agar", -1],
        [109918787, 226155763, "infer", -1],
        [109918787, 226155763, "haunt", -1],
        [109918787, 226155763, "wint", -1],
        [109918787, 226155763, "futu", -1],
        [109918787, 180871029, "", -1], //Essence
        [109918787, 197642084, "", 6], //Gadget
        [109918787, 88906941, "", 3], //Museum
        [109918787, 88906941, "", 4],
        [109918787, 88906941, "", 5],
        //cosmetic
        [111398483, 57339111, "", -1],
        [111398483, 57339111, "", 5], //Clothing
        [111398483, 57339111, "agar", -1],
        [111398483, 57339111, "infer", -1],
        [111398483, 57339111, "cyber", -1],
        [111398483, 57339111, "arcti", -1],
        [111398483, 57339111, "aranea", -1],
        //[111398483, 87563456], //Makeup
        //[111398483, 4998821], //Emote
        [111398483, 22212, "", 5], //Pet
        [111398483, 22212, "", 4],
        [111398483, 22212, "", 3],
        [111398483, 5532756, "", -1], //Sprint
        //[111398483, 5966629], //Title
        //Tali
        [137406174, 246461513, "radiant", -1],
        [137406174, 247368388, "radiant", -1],
        [137406174, 191434452, "radiant", -1],
        [137406174, 184609570, "radiant", -1],
        [137406174, 116095803, "radiant", -1],
        [137406174, 247306948, "radiant", -1],
        [137406174, 116054996, "radiant", -1],
        //Upgrade
        [208570357, 5128296, "intricate", -1], //Glyph
        [208570357, 94364868, "", -1], //Signet
        //[208570357, 132606149], //Weapon mod
        //Weapon
        [98273118, -1, "", 4],
        [98273118, 91624581, "", 3], //AR
        [98273118, 244175283, "", 3], //Blade
        [98273118, 41012030, "", 3], //Blood
        [98273118, 93626565, "", 3], //Chaos
        [98273118, 157308306, "", 3], //Ele
        [98273118, 4394078, "", 3], //Fist
        [98273118, 71672516, "", 3], //Hammer
        [98273118, 205690740, "", 3], //Pistol
        [98273118, 166678788, "", 3] //Shotgun
    ];
    
    public function AutoSearch() 
    {
        m_index = Math.random() * m_searches.length;
    }
    
    public function RunNextSearch()
    {
        if (m_searchCount > m_searches.length)
        {
            //Just stop searching once we've searched all the items in the list
            return;
        }
        
        var currentItem : Array = m_searches[m_index];
        
        Tradepost.m_SearchCriteria.m_ItemTypeVec = new Array();
        Tradepost.m_SearchCriteria.m_ItemTypeVec.push(currentItem[0]);
        
        Tradepost.m_SearchCriteria.m_ItemSubtypeVec = new Array();
        if (currentItem[1] != -1)
        {
            Tradepost.m_SearchCriteria.m_ItemSubtypeVec.push(currentItem[1]);
        }

        Tradepost.m_SearchCriteria.m_SearchString = currentItem[2];
		
        Tradepost.m_SearchCriteria.m_MinPowerLevel = currentItem[3]; //Rarity
        Tradepost.m_SearchCriteria.m_MaxPowerLevel = currentItem[3];
        
        
        //Some Defaults
        Tradepost.m_SearchCriteria.m_TokenTypeVec = new Array();
        Tradepost.m_SearchCriteria.m_MinStackSize = 0;
        Tradepost.m_SearchCriteria.m_MaxStackSize = 9999999;
        Tradepost.m_SearchCriteria.m_SellerName = "";
        Tradepost.m_SearchCriteria.m_SellerInstance = 0;
        Tradepost.m_SearchCriteria.m_UseExactName = false;
        Tradepost.m_SearchCriteria.m_MinPrice = 0;
        Tradepost.m_SearchCriteria.m_MaxPrice = 99999999;
        Tradepost.m_SearchCriteria.m_ItemPlacement = -1;
        Tradepost.m_SearchCriteria.m_ItemClassVec = new Array();
        
        
        Tradepost.MakeSearch();
        
        m_index++;
        if (m_index >= m_searches.length)
        {
            m_index = 0;
        }
        
        m_searchCount++;
    }
}