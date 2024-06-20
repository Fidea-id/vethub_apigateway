namespace Domain.Entities.Models.Clients
{
    public class ProductStockHistorical : BaseEntity
    {
        public int ProfileId { get; set; }
        public int ProductId { get; set; }
        public double Stock { get; set; }
        public double StockBefore { get; set; }
        public double StockAfter { get; set; }
        public double VolumeRemaining { get; set; }
        public string Type { get; set; }
    }
}
