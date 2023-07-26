using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Entities.Responses
{
    public class ProductDetailsResponse
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string Description { get; set; }
        public double Stock { get; set; }
        public string Volume { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public bool IsBundle { get; set; }
        public bool HasDiscount { get; set; }
        public double Price { get; set; }
        public IEnumerable<ProductBundleDetailResponse>? BundlesItems { get; set; }
        public IEnumerable<ProductDiscounts>? Discounts { get; set; }
    }
}
