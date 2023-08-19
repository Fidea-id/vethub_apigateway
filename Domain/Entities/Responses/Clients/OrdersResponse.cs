namespace Domain.Entities.Responses.Clients
{
    public class OrdersResponse
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public int Quantity { get; set; }
        public int? Discount { get; set; }
        public string Status { get; set; }
        public string? DiscountType { get; set; }
        public double TotalPrice { get; set; }
        public double TotalDiscountedPrice { get; set; }
    }
}
