namespace Domain.Entities.Responses.Clients
{
    public class OpnamePatientsDetailResponse
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public int MedicalRecordId { get; set; }
        public int PatientId { get; set; }
        public int OpnameId { get; set; }
        public string OpnameName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int EstimatedDays { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
    }
    public class OpnamePatientsDetailFullResponse
    {
        public OpnamePatientsDetailResponse OpnameDetail { get; set; }
        public MedicalRecordsDetailResponse MedicalDetail { get; set; }
    }
}
