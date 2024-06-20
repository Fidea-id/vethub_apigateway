namespace Domain.Entities.Responses.Clients
{
    public class OwnerStatistic
    {
        public int TotalBooking { get; set; }
        public DateTime LastVisit { get; set; }
        public long TotalPayment { get; set; }
    }
}
