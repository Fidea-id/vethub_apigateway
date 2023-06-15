using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IServicesRepository : IGenericRepository<Services, ServicesFilter>
    {
    }
}
