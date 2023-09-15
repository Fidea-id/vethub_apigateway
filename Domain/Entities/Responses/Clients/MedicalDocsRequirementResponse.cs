using Domain.Entities.Models.Clients;

namespace Domain.Entities.Responses.Clients
{
    public class MedicalDocsRequirementResponse
    {
        public Clinics ClinicData { get; set; }
        public Patients PatientData { get; set; }
        public IEnumerable<PatientsStatisticResponse> PatientLatestStatistic { get; set; }
        public Owners OwnerData { get; set; }
        public MedicalRecords MedicalData { get; set; }
        public string VetName { get; set; }
    }
}
