import com.GameInterface.Browser.Browser;
import com.GameInterface.DistributedValue;
import com.GameInterface.Game.Character;
import com.GameInterface.TradepostSearchResultData;
import mx.utils.Delegate;

class com.xeio.SwlMarket.MarketApi
{
    var m_Browser:Browser;
    var m_itemsToSubmit:Array = [];
    var m_lastSentItem:TradepostSearchResultData;
    var m_browserStuckTimeout:Number;
    var m_submitTimeout:Number;
    var m_disposeBrowser:Boolean;
    
    public function MarketApi()
    {
        
    }
    
    public function QueuePriceSubmit(newPrice: TradepostSearchResultData)
    {
        m_itemsToSubmit.push(newPrice);
        
        if(!m_Browser)
        {
            clearTimeout(m_submitTimeout);
            m_submitTimeout = setTimeout(Delegate.create(this, Upload), 1000);
        }
    }
    
    private function Upload()
    {
        if (m_disposeBrowser)
        {
            clearTimeout(m_submitTimeout);
            m_submitTimeout = setTimeout(Delegate.create(this, Upload), 1000);
            return;
        }
        
        if (!m_Browser)
        {
            m_Browser = new Browser(18, 100, 100);
            m_Browser.SignalBrowserShowPage.Connect(PageLoaded,  this);
        }
        
        m_lastSentItem = TradepostSearchResultData(m_itemsToSubmit.pop());
        if (!m_lastSentItem)
        {
            DisposeBrowser();
            return;
        }
        
        var url:String = DistributedValue.GetDValue("SwlMarket_Url") +
                            "/api/PriceUpload?name=" + escape(m_lastSentItem.m_Item.m_Name) +
                            "&price=" + m_lastSentItem["u_price"] +
                            "&rarity=" + m_lastSentItem.m_Item.m_Rarity +
                            "&category=" + m_lastSentItem.m_Item.m_ItemTypeGUI +
                            "&ext=" + (m_lastSentItem.m_Item.m_ColorLine == 43); //Extraordinary
        
        m_Browser.OpenURL(url);
        
        clearTimeout(m_browserStuckTimeout);
        m_browserStuckTimeout = setTimeout(Delegate.create(this, Timeout), 10000);
    }
    
    public function PageLoaded()
    {
        clearTimeout(m_browserStuckTimeout);
        m_Browser.Stop();
        
        if (this.m_itemsToSubmit.length == 0)
        {
            DisposeBrowser();
        }
        else
        {
            clearTimeout(m_submitTimeout);
            m_submitTimeout = setTimeout(Delegate.create(this, Upload), 1000);
        }
    }
    
    public function CheckDisposeBrowser()
    {
        if (m_disposeBrowser)
        {
            DisposeBrowser(false);
        }
    }
    
    private function DisposeBrowser()
    {
        if (Character.IsInReticuleMode())
        {
            m_Browser.CloseBrowser();
            m_Browser = undefined;
            m_disposeBrowser = false;
            
            if (this.m_itemsToSubmit.length > 0)
            {
                clearTimeout(m_submitTimeout);
                m_submitTimeout = setTimeout(Delegate.create(this, Upload), 1000);
            }
        }
        else
        {
            m_disposeBrowser = true;
        }
    }
    
    public function Timeout()
    {
        QueuePriceSubmit(m_lastSentItem);
        m_Browser.Stop();
        m_Browser.SignalBrowserShowPage.Disconnect(PageLoaded, this);
        DisposeBrowser();        
    }
}