namespace Domain.Entities.Models.Clients
{
    public class AppointmentsActivity : BaseEntity
    {
        public int AppointmentId { get; set; }
        public int CurrentStatusId { get; set; }
        public DateTime CurrentDate { get; set; }
        public string Note { get; set; }
        public int StaffId { get; set; }
    }
}
