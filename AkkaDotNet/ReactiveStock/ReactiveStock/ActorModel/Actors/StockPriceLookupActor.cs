using Akka.Actor;
using ReactiveStock.ActorModel.Messages;
using ReactiveStock.ExternalServices;
using System;

namespace ReactiveStock.ActorModel.Actors
{
    public class StockPriceLookupActor : ReceiveActor
    {

        private readonly IStockPriceServiceGateway _stockPriceServiceGateway;

        public StockPriceLookupActor(IStockPriceServiceGateway stockPriceServiceGateway)
        {
            _stockPriceServiceGateway = stockPriceServiceGateway;

            Receive<RefreshStockPriceMessage>(message => LookupStockPrice(message));
        }

        private void LookupStockPrice(RefreshStockPriceMessage message)
        {
            var latestPrice = _stockPriceServiceGateway.GetLatestPriceFor(message.StockSymbol);

            Sender.Tell(new UpdatedStockPriceMessage(latestPrice, DateTime.Now));
        }
    }
}
