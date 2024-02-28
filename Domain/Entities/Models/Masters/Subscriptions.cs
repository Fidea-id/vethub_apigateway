using Domain.Entities.Attributes;

namespace Domain.Entities.Models.Masters
{
    public class Subscriptions : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public int TotalMonth { get; set; }
        public double Price { get; set; }
        public double InitialDiscount { get; set; }
        public double DiscountedPrice { get; set; }
        public int PatientQuota { get; set; }
        public int MaxUser { get; set; }
        public bool IsClinic { get; set; }
    }
}
