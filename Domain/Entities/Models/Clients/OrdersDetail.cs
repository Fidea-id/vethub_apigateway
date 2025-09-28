namespace Domain.Entities.Models.Clients
{
    public class OrdersDetail : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int StaffId { get; set; }
        public double Quantity { get; set; }
        public double? Discount { get; set; }
        public string? DiscountType { get; set; }
        public double TotalPrice { get; set; }
    }
}
