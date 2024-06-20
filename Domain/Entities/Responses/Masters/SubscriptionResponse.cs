namespace Domain.Entities.Responses.Masters
{
    public class SubscriptionResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalMonth { get; set; }
        public int PatientQuota { get; set; }
        public int MaxUser { get; set; }
        public double Price { get; set; }
        public double InitialDiscount { get; set; }
        public double DiscountedPrice { get; set; }
        public bool IsClinic { get; set; }
    }
}
