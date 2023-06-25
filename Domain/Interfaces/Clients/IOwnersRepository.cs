using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IOwnersRepository : IGenericRepository<Owners, OwnersFilter>
    {
    }
}
