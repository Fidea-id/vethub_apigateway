using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Responses;

namespace Domain.Interfaces.Clients
{
    public interface IProductBundlesRepository : IGenericRepository<ProductBundles, ProductBundlesFilter>
    {
        Task<IEnumerable<ProductBundles>> GetProductBundlesByProduct(int productId, string dbName);
    }
}
