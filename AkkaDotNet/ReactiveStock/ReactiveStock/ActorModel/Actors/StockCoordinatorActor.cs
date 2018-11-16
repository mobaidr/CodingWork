using Akka.Actor;
using ReactiveStock.ActorModel.Messages;
using System.Collections.Generic;

namespace ReactiveStock.ActorModel.Actors
{
    class StockCoordinatorActor : ReceiveActor
    {
        private readonly IActorRef _chartingActor;
        private readonly Dictionary<string, IActorRef> _stockActors;

        public StockCoordinatorActor(IActorRef chartingActor)
        {
            _chartingActor = chartingActor;

            _stockActors = new Dictionary<string, IActorRef>();

            Receive<WatchStockMessage>(msg => WatchStockMessage(msg));
            Receive<UnWatchStockMessage>(msg => UnWatchStockMessage(msg));
        }

        private void WatchStockMessage(WatchStockMessage msg)
        {
            string stockSymbol = msg.StockSymbol;

            if(!_stockActors.ContainsKey(stockSymbol))
            {
                var childActor = Context.ActorOf(Props.Create(() => new StockActor(stockSymbol)), 
                    $"StockActor_{stockSymbol}");

                _stockActors.Add(stockSymbol, childActor);
            }

            _chartingActor.Tell(new AddChartSeriesMessage(stockSymbol));

            _stockActors[stockSymbol].Tell(new SubscribeToNewStockPricesMessage(_chartingActor));
        }

        private void UnWatchStockMessage(UnWatchStockMessage msg)
        {
            string stockSymbol = msg.StockSymbol;

            if (!_stockActors.ContainsKey(stockSymbol))
                return;

            _chartingActor.Tell(new RemoveChartSeriesMessage(stockSymbol));

            _stockActors[stockSymbol].Tell(new UnSubscribeFromNewStockPricesMessage(_chartingActor));
        }
    }
}
