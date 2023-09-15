namespace Domain.Entities.Responses.Clients
{
    public class MedicalRecordsResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int AppointmentId { get; set; }
        public int StaffId { get; set; }
        public int PatientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PaymentStatus { get; set; }
        public double Total { get; set; }
    }
}
