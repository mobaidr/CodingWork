namespace ReactiveStock.ExternalServices
{
    public interface IStockPriceServiceGateway
    {
        decimal GetLatestPriceFor(string stockSymbol);
    }
}
