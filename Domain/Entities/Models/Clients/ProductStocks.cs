namespace Domain.Entities.Models.Clients
{
    public class ProductStocks : BaseEntity
    {
        public int ProductId { get; set; }
        public double Stock { get; set; }
        public string Volume { get; set; }
    }
}
