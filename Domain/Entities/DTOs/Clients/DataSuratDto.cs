using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Entities.DTOs.Clients
{
    public class DataSuratDto<T>
    {
        public Clinics ClinicData { get; set; }
        public Patients PatientData { get; set; }
        public IEnumerable<PatientsStatisticResponse> PatientLatestStatistic { get; set; }
        public Owners OwnerData { get; set; }
        public MedicalRecords MedicalData { get; set; }
        public string VetName { get; set; }
        public string StaffName { get; set; }
        public T RequestData { get; set; }
    }
}
