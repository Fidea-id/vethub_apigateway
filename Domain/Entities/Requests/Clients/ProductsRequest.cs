namespace Domain.Entities.Requests.Clients
{
    public class ProductsRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Volume { get; set; }
        public string? VolumeUnit { get; set; }
        public int? CategoryId { get; set; }
        public int? Stock { get; set; }
        public int? Id { get; set; }
        public double? Price { get; set; }
        public bool? IsBundle { get; set; }
    }
}
