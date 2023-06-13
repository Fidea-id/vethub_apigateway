using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces
{
    public interface IProfileRepository : IGenericRepository<Profile, ProfileFilter>
    {
    }
}

