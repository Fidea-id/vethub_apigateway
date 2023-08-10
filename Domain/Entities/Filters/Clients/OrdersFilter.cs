namespace Domain.Entities.Filters.Clients
{
    public class OrdersFilter : BaseEntityFilter
    {
        public int? BranchId { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DueDate { get; set; }
        public int? ClientId { get; set; }
        public int? StaffId { get; set; }
        public string? ClientName { get; set; }
        public int? Quantity { get; set; }
        public int? Discount { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public string? DiscountType { get; set; }
        public double? TotalPrice { get; set; }
        public double? TotalDiscountedPrice { get; set; }
    }
}
