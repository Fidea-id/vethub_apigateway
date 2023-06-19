namespace Domain.Entities.Filters.Masters
{
    public class BillPaymentsFilter : BaseEntityFilter
    {
        public int? ClinicId { get; set; }
        public int? SubscriptionId { get; set; }
        public string? Period { get; set; }
        public string? Status { get; set; }
        public int? Price { get; set; }
        public int? Discount { get; set; }
        public int? Total { get; set; }
    }
}
