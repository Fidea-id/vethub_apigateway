namespace Domain.Entities.Models.Clients
{
    public class PatientsStatistic: BaseEntity
    {
        public int PatientId { get; set; }
        public int StaffId { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
        public string Value { get; set; }
    }
}
