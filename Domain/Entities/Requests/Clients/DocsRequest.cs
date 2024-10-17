namespace Domain.Entities.Requests.Clients
{
    public class DocsKematianRequest
    {
        public int MedicalRecordsId { get; set; }
        public string DiedReason { get; set; }
        public DateTime DiedDate { get; set; }
        public DateTime DiedTime { get; set; }
        public string DiedBuriedInfo { get; set; }
    }

    public class DocsPermintaanPulangRequest
    {
        public int MedicalRecordsId { get; set; }
        public string? OwnerPhoneNumber { get; set; }
        public string OwnerIdNumber { get; set; }
    }

    public class DocsRujukanRequest
    {
        public int MedicalRecordsId { get; set; }
        public string ClinicRefferalName { get; set; }
        public IEnumerable<string> MedicalAction { get; set; }
    }

    public class DocsTindakanRequest
    {
        public int MedicalRecordsId { get; set; }
        public string VetNote { get; set; }
        public double DepositAmount { get; set; }
        public string OwnerIdNumber { get; set; }
        public IEnumerable<string> MedicalDiagnose { get; set; }
        public IEnumerable<string> MedicalAction { get; set; }
    }
}
