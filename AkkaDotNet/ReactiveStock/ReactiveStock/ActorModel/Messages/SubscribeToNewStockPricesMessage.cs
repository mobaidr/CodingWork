using Akka.Actor;

namespace ReactiveStock.ActorModel.Messages
{
    public class SubscribeToNewStockPricesMessage
    {
        public IActorRef Subscriber { get; private set; }

        public SubscribeToNewStockPricesMessage(IActorRef subscriber)
        {
            Subscriber = subscriber;
        }
    }
}
