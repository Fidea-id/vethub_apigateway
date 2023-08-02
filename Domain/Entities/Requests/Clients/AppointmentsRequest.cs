namespace Domain.Entities.Requests.Clients
{
    public class AppointmentsRequest
    {
        public int? OwnersId { get; set; }
        public int? PatientsId { get; set; }
        public DateTime? Date { get; set; }
        public int? StaffId { get; set; }
        public int? ServiceId { get; set; }
        public int? StatusId { get; set; }
        public string? Notes { get; set; }
    }

    public class AppointmentsRequestChangeStatus
    {
        public int? Id { get; set; }
        public int? StatusId { get; set; }
        public int? StaffId { get; set; } // updated by staff
        public string? Notes { get; set; }
    }
}
