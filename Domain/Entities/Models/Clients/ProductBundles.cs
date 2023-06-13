namespace Domain.Entities.Models.Clients
{
    public class ProductBundles : BaseEntity
    {
        public int BundleId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
    }
}
