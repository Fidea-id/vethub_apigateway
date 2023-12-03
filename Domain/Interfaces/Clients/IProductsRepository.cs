using Domain.Entities.DTOs.Clients;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Responses;

namespace Domain.Interfaces.Clients
{
    public interface IProductsRepository : IGenericRepository<Products, ProductsFilter>
    {
        Task<ProductDetailsResponse> GetProductDetails(int id, string dbName);
        Task<IEnumerable<ProductDetailsResponse>> GetListProductDetails(string dbName);
        Task<CheckValidDTO> CheckProductValidList(IEnumerable<BulkProduct> data, string dbName);
    }
}
