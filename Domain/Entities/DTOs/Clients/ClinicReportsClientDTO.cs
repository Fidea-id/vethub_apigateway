namespace Domain.Entities.DTOs.Clients
{
    public class ClinicReportsClientDTO
    {
        public string Entity { get; set; }
        public string ClinicName { get; set; }
        public int? TotalPatients { get; set; }
        public int? TotalAppointments { get; set; }
        public int? TotalMedicalRecords { get; set; }
        public double? TotalRevenue { get; set; }
        public DateTime? LastActivity { get; set; }
    }
}
