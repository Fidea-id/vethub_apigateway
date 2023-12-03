using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("ProductStockHistorical")]
    public class ProductStockHistoricalXPO : XPLiteObject
    {
        public ProductStockHistoricalXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }

        public int ProfileId { get; set; }
        public int ProductId { get; set; }
        public double Stock { get; set; }
        public double StockBefore { get; set; }
        public double StockAfter { get; set; }
        public double VolumeRemaining { get; set; }
        public string Type { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
