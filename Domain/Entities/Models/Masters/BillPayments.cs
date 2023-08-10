using Domain.Entities.Attributes;

namespace Domain.Entities.Models.Masters
{
    public class BillPayments : BaseEntity
    {
        public int UserId { get; set; }
        public int SubscriptionId { get; set; }
        [MaxLength(100)]
        public string Period { get; set; }
        public int StatusId { get; set; }
        public double Price { get; set; }
        public int MaxUser { get; set; }
        public double? Discount { get; set; }
        public double Total { get; set; }
    }
}
