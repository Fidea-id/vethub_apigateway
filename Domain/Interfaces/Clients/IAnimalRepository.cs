using Domain.Entities;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IAnimalRepository : IGenericRepository<Animals, BaseEntityFilter>
    {
    }
}
