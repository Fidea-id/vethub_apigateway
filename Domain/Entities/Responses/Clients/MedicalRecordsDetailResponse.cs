using Domain.Entities.Models.Clients;

namespace Domain.Entities.Responses.Clients
{
    public class MedicalRecordsHistoryResponse
    {
        public DateTime Date { get; set; }
        public MedicalRecordsDetailResponse Detail { get; set; }
    }
    public class MedicalRecordsDetailResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public Appointments? Appointments { get; set; }
        public Services? Services { get; set; }
        public Patients? Patients { get; set; }
        public Owners? Owners { get; set; }
        public Profile? Staff { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPrice { get; set; }
        public double TotalPaid { get; set; }
        public string? DiscountMethod { get; set; }
        public double? DiscountValue { get; set; }
        public double? DiscountTotal { get; set; }
        public double? TotalDiscounted { get; set; }
        public IEnumerable<MedicalRecordsNotes>? Notes { get; set; }
        public IEnumerable<MedicalRecordsPrescriptions>? Prescriptions { get; set; }
        public IEnumerable<MedicalRecordsDiagnoses>? Diagnoses { get; set; }
        public OpnameDetailResponse? OpnameDetail { get; set; }
        public string StatusPayment { get; set; }
    }
    public class MedicalRecordsMinResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Prescriptions { get; set; }
        public string Services { get; set; }
        public string Diagnoses { get; set; }
        public string StatusPayment { get; set; }
    }
}
