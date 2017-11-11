import com.GameInterface.Chat;
import com.GameInterface.DistributedValue;
import com.GameInterface.Inventory;
import com.GameInterface.ShopInterface;
import com.GameInterface.Tradepost;
import com.GameInterface.TradepostSearchResultData;
import com.Utils.Archive;
import com.Utils.LDBFormat;
import com.xeio.SwlMarket.MarketApi;


class com.xeio.SwlMarket.SwlMarket
{    
	private var m_swfRoot: MovieClip;
	    
    private var m_marketApi:MarketApi;
	
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
        
        Tradepost.SignalSearchResult.Connect(SlotResultsReceived, this);
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