using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IMedicalRecordsRepository : IGenericRepository<MedicalRecords, MedicalRecordsFilter>
    {
        Task<IEnumerable<MonthlyDataChart>> GetVisitYearly(string dbName);
        Task<string> GetLatestCode(string dbName);
    }
}
