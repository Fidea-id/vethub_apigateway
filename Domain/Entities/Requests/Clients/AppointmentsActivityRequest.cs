namespace Domain.Entities.Requests.Clients
{
    public class AppointmentsActivityRequest
    {
        public int AppointmentId { get; set; }
        public int CurrentStatusId { get; set; }
        public DateTime CurrentDate { get; set; }
        public string? Note { get; set; }
        public int StaffId { get; set; }
    }
}
