using System;

namespace ReactiveStock.ActorModel.Messages
{
    public class UpdatedStockPriceMessage
    {
        public decimal NewPrice { get; private set; }
        public DateTime Date { get; private set; }

        public UpdatedStockPriceMessage(decimal price, DateTime date)
        {
            NewPrice = price;
            Date = date;
        }
    }
}
