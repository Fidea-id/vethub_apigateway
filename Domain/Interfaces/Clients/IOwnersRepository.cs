using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces
{
    public interface IOwnersRepository : IGenericRepository<Owners, OwnersFilter>
    {
    }
}
