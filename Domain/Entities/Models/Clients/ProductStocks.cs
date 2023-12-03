namespace Domain.Entities.Models.Clients
{
    public class ProductStocks : BaseEntity
    {
        public int ProductId { get; set; }
        public double Stock { get; set; }
        public double Volume { get; set; }
        public string VolumeUnit { get; set; }
        public double VolumeRemaining { get; set; }
    }
}
