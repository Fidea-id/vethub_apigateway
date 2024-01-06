namespace Domain.Entities.Models.Clients
{
    public class MedicalRecordsDiagnoses : BaseEntity
    {
        public int MedicalRecordsId { get; set; }
        public string Diagnose { get; set; }
        public string Prognose { get; set; }
        public string DiagnoseType { get; set; }
        //public double TotalPrice { get; set; }
    }
}
