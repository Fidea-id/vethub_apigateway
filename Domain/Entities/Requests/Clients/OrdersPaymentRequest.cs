namespace Domain.Entities.Requests.Clients
{
    public class OrdersPaymentRequest
    {
        public int OrderId { get; set; }
        public int PaymentMethodId { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }
        public string? DiscountMethod { get; set; }
        public double? DiscountValue { get; set; }
        public double? DiscountTotal { get; set; }
        public string? Type { get; set; }
        public string? Note { get; set; }
    }
}
