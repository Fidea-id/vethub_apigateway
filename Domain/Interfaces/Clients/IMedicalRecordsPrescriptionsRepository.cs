using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IMedicalRecordsPrescriptionsRepository : IGenericRepository<MedicalRecordsPrescriptions, MedicalRecordsPrescriptionsFilter>
    {
        Task<IEnumerable<MedicalRecordsPrescriptions>> GetByMedicalRecordId(string dbName, int medicalRecordsId);
    }
}
