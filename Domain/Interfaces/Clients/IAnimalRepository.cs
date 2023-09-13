using Domain.Entities.Filters;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IAnimalRepository : IGenericRepository<Animals, NameBaseEntityFilter>
    {
        Task<Animals> GetByName(string dbName, string name);
    }
}
