namespace Domain.Entities.DTOs.Clients
{
    public class BulkProduct
    {
        public int row { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public int categoryId { get; set; }
        public double price { get; set; }
        public double stock { get; set; }
        public double volume { get; set; }
        public string unit { get; set; }
    }
    public class BulkProducts
    {
        public IEnumerable<BulkProduct> listData { get; set; }
    }
}
