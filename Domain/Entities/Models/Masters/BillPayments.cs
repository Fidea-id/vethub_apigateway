namespace Domain.Entities.Models.Masters
{
    public class BillPayments : BaseEntity
    {
        public int ClinicId { get; set; }
        public int SubscriptionId { get; set; }
        public string Period { get; set; }
        public string Status { get; set; }
        public int Price { get; set; }
        public int? Discount { get; set; }
        public int Total { get; set; }
    }
}
