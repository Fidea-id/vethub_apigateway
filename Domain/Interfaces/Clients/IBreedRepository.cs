using Domain.Entities;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IBreedRepository : IGenericRepository<Breeds, BaseEntityFilter>
    {
    }
}
