using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IMedicalRecordsNotesRepository : IGenericRepository<MedicalRecordsNotes, MedicalRecordsNotesFilter>
    {
        Task<MedicalRecordsNotes> CheckRecordType(string dbName, int medicalRecordsId, string type);
        Task<IEnumerable<MedicalRecordsNotes>> GetByMedicalRecordId(string dbName, int medicalRecordsId);
    }
}
