using Domain.Entities.Models.Clients;

namespace Domain.Entities.Responses.Clients
{
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
        public IEnumerable<MedicalRecordsNotes>? Notes { get; set; }
        public IEnumerable<MedicalRecordsPrescriptions>? Prescriptions { get; set; }
        public IEnumerable<MedicalRecordsDiagnoses>? Diagnoses { get; set; }
    }
}
