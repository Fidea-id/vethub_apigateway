namespace Domain.Entities.Requests.Clients
{
    public class ProductAsBundleRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public double? Price { get; set; }
        public bool? IsBundle { get; set; }
        public IEnumerable<ProductItemBundle>? BundleItem { get; set; }
    }
    public class ProductItemBundle
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
