using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IProductDiscountsRepository : IGenericRepository<ProductDiscounts, ProductDiscountsFilter>
    {
        Task<IEnumerable<ProductDiscounts>> GetProductDiscountByProduct(int productId, string dbName);
    }
}
