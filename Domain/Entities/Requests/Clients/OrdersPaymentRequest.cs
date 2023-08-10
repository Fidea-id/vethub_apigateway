namespace Domain.Entities.Requests.Clients
{
    public class OrdersPaymentRequest
    {
        public int OrderId { get; set; }
        public int PaymentMethodId { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }
    }
}
