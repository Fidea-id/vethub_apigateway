using Domain.Entities;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IProductStockHistoricalRepository : IGenericRepository<ProductStockHistorical, BaseEntityFilter>
    {
    }
}
