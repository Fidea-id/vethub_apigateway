namespace Domain.Entities.Filters.Clients
{
    public class ProductsFilter : BaseEntityFilter
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public double? Price { get; set; }
        public bool? IsBundle { get; set; }
    }
}
