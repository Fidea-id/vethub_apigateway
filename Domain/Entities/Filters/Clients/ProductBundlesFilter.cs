namespace Domain.Entities.Filters.Clients
{
    public class ProductBundlesFilter : BaseEntityFilter
    {
        public int? ProductId { get; set; }
        public int? ItemId { get; set; }
        public double? Quantity { get; set; }
    }
}
