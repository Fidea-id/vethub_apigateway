namespace Domain.Entities.Requests.Clients
{
    public class ProductStockHistoricalRequest
    {
        public int ProfileId { get; set; }
        public int ProductId { get; set; }
        public double Stock { get; set; }
        public double StockBefore { get; set; }
        public double StockAfter { get; set; }
        public string Type { get; set; }
    }
}
