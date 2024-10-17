namespace Domain.Entities.Filters.Clients
{
    public class OrdersPaymentFilter : BaseEntityFilter
    {
        public int? OrderId { get; set; }
        public int? PaymentMethodId { get; set; }
        public DateTime? Date { get; set; }
        public double? Total { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }
    }
}
