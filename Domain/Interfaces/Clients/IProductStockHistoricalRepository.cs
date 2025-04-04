using Domain.Entities;
using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IProductStockHistoricalRepository : IGenericRepository<ProductStockHistorical, BaseEntityFilter>
    {
        Task<IEnumerable<ProductStockHistoricalResponse>> GetByProductHistoricalAsync(string dbName);
    }
}
