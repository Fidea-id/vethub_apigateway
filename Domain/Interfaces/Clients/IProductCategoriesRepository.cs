using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IProductCategoriesRepository : IGenericRepository<ProductCategories, ProductCategoriesFilter>
    {
    }
}
