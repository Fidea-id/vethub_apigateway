using Domain.Entities.Filters;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IProductStockRepository : IGenericRepository<ProductStocks, NameBaseEntityFilter>
    {
        Task UpdateMinStock(int productId, int quantity, string dbName);
        Task UpdateAddStock(int productId, int quantity, string dbName);
    }
}
