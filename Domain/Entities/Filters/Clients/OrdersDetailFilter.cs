namespace Domain.Entities.Filters.Clients
{
    public class OrdersDetailFilter : BaseEntityFilter
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? StaffId { get; set; }
        public int? Quantity { get; set; }
        public int? Discount { get; set; }
        public string? DiscountType { get; set; }
        public double? TotalPrice { get; set; }
    }
}
