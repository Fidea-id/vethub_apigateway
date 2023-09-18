namespace Domain.Entities.Requests.Clients
{
    public class ProductsRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Volume { get; set; }
        public int? CategoryId { get; set; }
        public int? Stock { get; set; }
        public double? Price { get; set; }
        public bool? IsBundle { get; set; }
    }
}
