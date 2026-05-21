using Domain.Entities.Models.Clients;
using Domain.Entities.Filters.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IJournalEntriesRepository : IGenericRepository<JournalEntries, JournalEntriesFilter>
    {
    }
}
