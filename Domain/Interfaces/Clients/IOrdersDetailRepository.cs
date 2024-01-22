using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IOrdersDetailRepository : IGenericRepository<OrdersDetail, OrdersDetailFilter>
    {
        Task<IEnumerable<OrdersDetailResponse>> GetByOrderId(string dbName, int id);
    }
}
