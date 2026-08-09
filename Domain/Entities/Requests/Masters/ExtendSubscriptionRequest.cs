namespace Domain.Entities.Requests.Masters
{
    public class ExtendSubscriptionRequest
    {
        public int UserId { get; set; }
        public DateTime EndDate { get; set; }
    }
}
