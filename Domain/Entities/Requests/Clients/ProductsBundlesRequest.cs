namespace Domain.Entities.Requests.Clients
{
    public class ProductsBundlesRequest
    {
        public int BundleId { get; set; }
        public int ItemId { get; set; }
        public double Quantity { get; set; }
    }
}
