namespace ReactiveStock.ActorModel.Messages
{
    public class RefreshStockPriceMessage
    {
        public string StockSymbol { get; private set; }

        public RefreshStockPriceMessage(string stockSymbol)
        {
            this.StockSymbol = stockSymbol;
        }
    }
}
