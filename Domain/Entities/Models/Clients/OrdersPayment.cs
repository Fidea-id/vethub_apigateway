namespace Domain.Entities.Models.Clients
{
    public class OrdersPayment : BaseEntity
    {
        public int OrderId { get; set; }
        public int PaymentMethodId { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
    }
}
