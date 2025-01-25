using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IMedicalRecordsRepository : IGenericRepository<MedicalRecords, MedicalRecordsFilter>
    {
        Task<IEnumerable<MonthlyDataChart>> GetVisitYearly(string dbName, string? dateFilter);
        Task<MedicalRecordsDetailResponse> GetDetailById(string dbName, int id, string flag);
        Task<RevenueDataResponse> GetSalesDetail(string dbName, string query);
        Task<MedicalRecords> GetByAppointmentId(string dbName, int appointmentId);
        Task<string> GetLatestCode(string dbName);
        Task<IEnumerable<MonthlyDataChart>> GetTotalMedicalSales(string dbName, string dateFilter);
    }
}
