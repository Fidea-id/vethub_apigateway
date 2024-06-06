using Domain.Entities.Responses.Masters;

namespace Domain.Entities.Responses.Clients
{
    public class OrderFullResponse
    {
        //public int BranchId { get; set; }
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string Type { get; set; }
        public int TotalQuantity { get; set; }
        public string Status { get; set; }
        public double TotalPrice { get; set; }
        public double TotalDiscountedPrice { get; set; }
        public double TotalDiscount { get; set; }
        public IEnumerable<OrdersDetailResponse>? OrderProducts { get; set; }
        public IEnumerable<OrdersPaymentResponse>? OrderPayments { get; set; }
        public ClientClinicResponse? ClinicData { get; set; }
    }
    public class OrdersDetailResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
        public string? DiscountType { get; set; }
        public double TotalPrice { get; set; }
    }
    public class OrdersPaymentResponse
    {
        public int PaymentMethodId { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double LessTotal { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
