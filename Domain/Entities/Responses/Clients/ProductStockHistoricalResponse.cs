namespace Domain.Entities.Responses.Clients
{
    public class ProductStockHistoricalResponse
    {
        public int ProfileId { get; set; }
        public string ProfileName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Stock { get; set; }
        public double StockBefore { get; set; }
        public double StockAfter { get; set; }
        public double VolumeRemaining { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }
}
