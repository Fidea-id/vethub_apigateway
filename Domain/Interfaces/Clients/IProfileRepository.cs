using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IProfileRepository : IGenericRepository<Profile, ProfileFilter>
    {
        Task<Profile> GetByGlobalId(string dbName, int id);
        Task<Profile> GetByEmail(string dbName, string email);
        Task<Profile> GetOwner(string dbName);
    }
}

