namespace Domain.Entities.Models.Clients
{
    public class ProductBundles : BaseEntity
    {
        public int ProductId { get; set; }
        public double Quantity { get; set; }
    }
}
