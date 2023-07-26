namespace Domain.Entities.Models.Clients
{
    public class ProductBundles : BaseEntity
    {
        public int BundleId { get; set; }
        public int ItemId { get; set; }
        public double Quantity { get; set; }
    }
}
