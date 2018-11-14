using System;

namespace ReactiveStock.ExternalServices
{
    public class RandomStockPriceServiceGateway : IStockPriceServiceGateway
    {
        private decimal _lastRandomPrice = 20;
        private readonly Random _random = new Random();

        public decimal GetLatestPriceFor(string stockSymbol)
        {
            var newPrice = _lastRandomPrice + _random.Next(-5, 5);

            _lastRandomPrice = Math.Min(Math.Max(5, newPrice), 45);

            return _lastRandomPrice;
        }
    }
}
