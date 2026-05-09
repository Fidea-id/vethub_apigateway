using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IChartOfAccountsRepository : IGenericRepository<ChartOfAccounts, ChartOfAccountsFilter>
    {
    }
}
