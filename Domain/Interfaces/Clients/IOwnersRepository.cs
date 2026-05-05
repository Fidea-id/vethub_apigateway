using Domain.Entities.DTOs;
using Domain.Entities.DTOs.Clients;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IOwnersRepository : IGenericRepository<Owners, OwnersFilter>
    {
        Task<Owners> ReadByPatientIdAsync(int id, string dbName);
        Task<DataResultDTO<OwnerListSpending>> GetOwnerWithSpending(OwnersSpendingFilter filters, string dbName);
        Task<IEnumerable<MonthlyDataChart>> GetOwnerChart(string dbName, string dateFilter);
        Task<CheckValidDTO> CheckOwnerPatientValidList(IEnumerable<BulkOwnerPatient> data, string dbName);
    }
}
