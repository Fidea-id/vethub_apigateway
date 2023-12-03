using Domain.Entities.Responses.Clients;

namespace Domain.Entities.Responses
{
    public class ProductDetailsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Stock { get; set; }
        public double Volume { get; set; }
        public double VolumeRemaining { get; set; }
        public string VolumeUnit { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public bool IsBundle { get; set; }
        public bool IsActive { get; set; }
        public bool HasDiscount { get; set; }
        public double Price { get; set; }
        public IEnumerable<ProductBundleDetailResponse>? BundlesItems { get; set; }
        public int BundlesItemsCount { get; set; }
        public IEnumerable<ProductDiscountDetailResponse>? Discounts { get; set; }
    }
}
