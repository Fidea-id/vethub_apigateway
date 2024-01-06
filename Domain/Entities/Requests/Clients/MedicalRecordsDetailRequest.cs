namespace Domain.Entities.Requests.Clients
{
    public class MedicalRecordsDetailRequest
    {
        public int MedicalRecordsId { get; set; }
        public MedicalRecordsNotesRequest? Notes { get; set; }
        public IEnumerable<MedicalRecordsDiagnosesRequest> Diagnoses { get; set; }
        public IEnumerable<MedicalRecordsPrescriptionsRequest> Prescriptions { get; set; }
    }
    public class MedicalRecordsDiagnosesRequest
    {
        public string Diagnose { get; set; }
        public string Prognose { get; set; }
        public string DiagnoseType { get; set; }
        //public double TotalPrice { get; set; }
    }
}
