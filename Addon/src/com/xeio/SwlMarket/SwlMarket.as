import com.GameInterface.DistributedValueBase;
import com.GameInterface.Tradepost;
import com.GameInterface.TradepostSearchResultData;
import com.Utils.Archive;
import com.Utils.LDBFormat;
import com.xeio.SwlMarket.AutoSearch;
import com.xeio.SwlMarket.MarketApi
import com.xeio.SwlMarket.Utils;
import mx.utils.Delegate;


class com.xeio.SwlMarket.SwlMarket
{    
	private var m_swfRoot: MovieClip;
	    
    private var m_marketApi:MarketApi;
    private var m_autoSearch:AutoSearch;
    
    private var m_interval:Number;
	
	public static function main(swfRoot:MovieClip):Void 
	{
		var swlMarket = new SwlMarket(swfRoot);
		
		swfRoot.onLoad = function() { swlMarket.OnLoad(); };
		swfRoot.OnUnload =  function() { swlMarket.OnUnload(); };
		swfRoot.OnModuleActivated = function(config:Archive) { swlMarket.Activate(config); };
		swfRoot.OnModuleDeactivated = function() { return swlMarket.Deactivate(); };
	}
	
    public function SwlMarket(swfRoot: MovieClip) 
    {
		m_swfRoot = swfRoot;
    }
	
	public function OnUnload()
	{
        clearInterval(m_interval);
        m_interval = 0;
	}
	
	public function Activate(config: Archive)
	{
	}
	
	public function Deactivate(): Archive
	{
		var archive: Archive = new Archive();			
		return archive;
	}
	
	public function OnLoad()
	{
        if (LDBFormat.GetCurrentLanguageCode() != "en")
        {
            com.GameInterface.Chat.SignalShowFIFOMessage.Emit("Non-English client detected, disabling SwlMarket", 0);
            return;
        }
        m_marketApi = new MarketApi();
        m_autoSearch = new AutoSearch();
        
        Tradepost.SignalSearchResult.Connect(SlotResultsReceived, this);
        if (DistributedValueBase.GetDValue("SwlMarket_RunAutoSearch") && LastSearchExpired())
        {
            if (m_interval != 0)
            {
                clearInterval(m_interval);
            }
            m_interval = setInterval(Delegate.create(m_autoSearch, m_autoSearch.RunNextSearch), 20000);
        }
	}
    
    private function LastSearchExpired() : Boolean
    {
        var lastSearch:Number = DistributedValueBase.GetDValue("SwlMarket_LastSearchTime");
        return (new Date()).valueOf() > lastSearch + 14 * 60 * 60 * 1000;
    }
    
    private function SlotResultsReceived() : Void
    {
        var minimumPrices:Array = new Array();
        
        for (var i in Tradepost.m_SearchResults)
        {
            var result:TradepostSearchResultData = Tradepost.m_SearchResults[i];
            
            var price:Number = result.m_TokenType1_Amount;
            if (result.m_Item.m_StackSize > 1)
            {
                price = Math.round(price / result.m_Item.m_StackSize);
            }
            
            var currentEntry = Utils.FirstOrNull(minimumPrices, function(p) { return p.m_Item.m_Name == result.m_Item.m_Name && p.m_Item.m_Rarity == result.m_Item.m_Rarity; })
            if (!currentEntry || currentEntry.u_price > price)
            {
                Utils.Remove(minimumPrices, currentEntry);
                result["u_price"] = price;
                minimumPrices.push(result);
            }
        }

        for (var i in minimumPrices)
        {
            var lowestPrice:TradepostSearchResultData = TradepostSearchResultData(minimumPrices[i]);
            m_marketApi.QueuePriceSubmit(lowestPrice);
        }
    }
    	
}