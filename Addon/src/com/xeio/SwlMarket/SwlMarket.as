import com.GameInterface.DistributedValueBase;
import com.GameInterface.Tradepost;
import com.GameInterface.TradepostSearchResultData;
import com.Utils.Archive;
import com.Utils.LDBFormat;
import com.xeio.SwlMarket.AutoSearch;
import com.xeio.SwlMarket.MarketApi
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
        if (DistributedValueBase.GetDValue("SwlMarket_RunAutoSearch"))
        {
            m_interval = setInterval(Delegate.create(m_autoSearch, m_autoSearch.RunNextSearch), 60000);
        }
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
            
            if (!minimumPrices[result.m_Item.m_Name] || minimumPrices[result.m_Item.m_Name].u_price > price)
            {
                result["u_price"] = price;
                minimumPrices[result.m_Item.m_Name] = result;
            }
        }        

        for (var i in minimumPrices)
        {
            var lowestPrice:TradepostSearchResultData = TradepostSearchResultData(minimumPrices[i]);
            m_marketApi.QueuePriceSubmit(lowestPrice);
        }
    }
    	
}