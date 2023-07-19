using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Responses;

namespace Domain.Interfaces.Clients
{
    public interface IProductCategoriesRepository : IGenericRepository<ProductCategories, ProductCategoriesFilter>
    {
    }
}
