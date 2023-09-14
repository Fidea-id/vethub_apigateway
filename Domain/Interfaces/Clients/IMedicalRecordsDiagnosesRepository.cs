using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IMedicalRecordsDiagnosesRepository : IGenericRepository<MedicalRecordsDiagnoses, MedicalRecordsDiagnosesFilter>
    {
        Task<IEnumerable<MedicalRecordsDiagnoses>> GetByMedicalRecordId(string dbName, int medicalRecordsId);
    }
}
