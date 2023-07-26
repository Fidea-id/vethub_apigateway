using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IProductBundlesRepository : IGenericRepository<ProductBundles, ProductBundlesFilter>
    {
        Task<IEnumerable<ProductBundleDetailResponse>> GetProductBundlesByProduct(int productId, string dbName);
    }
}
