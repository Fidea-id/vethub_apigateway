using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IMedicalRecordsPrescriptionsRepository : IGenericRepository<MedicalRecordsPrescriptions, MedicalRecordsPrescriptionsFilter>
    {
        Task<IEnumerable<MedicalRecordsPrescriptions>> GetByMedicalRecordId(string dbName, int medicalRecordsId);
        Task<IEnumerable<FrequentDiagnoseMeds>> GetMedsFrequency(string dbName, string date);
        //Task<> GetSumPaid(string dbName, int medicalRecordsId);

    }
}
