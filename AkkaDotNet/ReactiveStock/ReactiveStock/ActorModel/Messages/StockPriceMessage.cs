using System;

namespace ReactiveStock.ActorModel.Messages
{
    class StockPriceMessage
    {
        public decimal StockPrice { get; private set; }
        public string StockSymbol { get; private set; } 
        public DateTime  Date { get; private set; }

        public StockPriceMessage(string stockSymbol, decimal stockPrice, DateTime date)
        {
            StockSymbol = stockSymbol;
            StockPrice = stockPrice;
            Date = date;
        }
    }
}
