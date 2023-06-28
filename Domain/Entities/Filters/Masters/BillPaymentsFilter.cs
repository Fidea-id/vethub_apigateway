namespace Domain.Entities.Filters.Masters
{
    public class BillPaymentsFilter : BaseEntityFilter
    {
        public int? ClinicId { get; set; }
        public int? SubscriptionId { get; set; }
        public string? Period { get; set; }
        public string? Status { get; set; }
        public double? Price { get; set; }
        public double? Discount { get; set; }
        public double? Total { get; set; }
    }
}
