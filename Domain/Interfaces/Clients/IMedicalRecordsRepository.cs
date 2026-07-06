using Domain.Entities.DTOs.Clients;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IMedicalRecordsRepository : IGenericRepository<MedicalRecords, MedicalRecordsFilter>
    {
        Task<IEnumerable<MonthlyDataChart>> GetVisitYearly(string dbName, string? dateFilter);
        Task<IEnumerable<MedicalRecordsDetailResponse>> GetDetailList(string dbName, string flag);
        Task<MedicalRecordsDetailResponse> GetDetailById(string dbName, int id, string flag);
        Task<PharmacyMedicalRecordDetailResponse> GetPharmacyDetailById(string dbName, int id);
        Task<int> GetRevenuePagedData(string dbName);
        Task<IEnumerable<RevenueResponse>> GetRevenueData(string dbName);
        Task<RevenueSummaryResponse> GetRevenueDataSummary(string dbName, DateTime? startDate, DateTime? endDate);
        Task<IEnumerable<string>> GetRevenueDataFilter(string dbName, string filterField);
        Task<RevenueDataResponse> GetSalesDetail(string dbName, string query);
        Task<MedicalRecords> GetByAppointmentId(string dbName, int appointmentId);
        Task<string> GetLatestCode(string dbName);
        Task<IEnumerable<MonthlyDataChart>> GetTotalMedicalSales(string dbName, string dateFilter);
        Task<List<MedicalRecordServicesReportDto>> GetMedicalRecordServicesReportAsync(string dbName, string? startDate = null, string? endDate = null, int? staffId = null, bool? onlyModifiedPrices = null);
        Task<IEnumerable<DoctorPerformanceRawDto>> GetDoctorPerformanceRaw(string dbName, int year);
    }
}
