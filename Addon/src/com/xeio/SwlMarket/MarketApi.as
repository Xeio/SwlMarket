import com.GameInterface.Browser.Browser;
import com.GameInterface.DistributedValue;
import com.GameInterface.TradepostSearchResultData;
import mx.utils.Delegate;

class com.xeio.SwlMarket.MarketApi
{
    var m_Browser:Browser;
    var m_itemsToSubmit:Array = [];
    
    public function MarketApi()
    {
        
    }
    
    public function QueuePriceSubmit(newPrice: TradepostSearchResultData)
    {
        m_itemsToSubmit.push(newPrice);
        
        if(!m_Browser)
        {
            Upload();
        }
    }
    
    private function Upload()
    {
        if (!m_Browser)
        {
            m_Browser = new Browser(_global.Enums.WebBrowserStates.e_BrowserMode_Browser, 100, 100);
            m_Browser.SignalBrowserShowPage.Connect(PageLoaded,  this);
        }
        
        var item:TradepostSearchResultData = TradepostSearchResultData(m_itemsToSubmit.pop());
        if (!item)
        {
            m_Browser.CloseBrowser();
            m_Browser = undefined;
            return;
        }
        
        var url:String = DistributedValue.GetDValue("SwlMarket_Url") +
                            "/api/PriceUpload?name=" + escape(item.m_Item.m_Name) +
                            "&price=" + item.m_TokenType1_Amount +
                            "&expiresIn=" + item.m_ExpireDate +
                            "&rarity=" + item.m_Item.m_Rarity +
                            "&apiKey=" + escape(DistributedValue.GetDValue("SwlMarket_ApiKey"));
        
        m_Browser.OpenURL(url);
    }
    
    public function PageLoaded()
    {
        if (this.m_itemsToSubmit.length == 0)
        {
            m_Browser.CloseBrowser();
            m_Browser = undefined;
        }
        else
        {
            setTimeout(Delegate.create(this, Upload), 1000);
        }
    }
}