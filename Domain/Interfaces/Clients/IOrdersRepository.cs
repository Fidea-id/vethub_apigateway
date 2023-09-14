using Domain.Entities.DTOs;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IOrdersRepository : IGenericRepository<Orders, OrdersFilter>
    {
        Task<DashboardOrderResponse> GetOrdersDashboard(string dbName);
        Task<string> GetLatestCode(string dbName);
        Task<DataResultDTO<OrdersResponse>> GetOrdersList(string dbName, OrdersFilter filter);
        Task<IEnumerable<OrderFullResponse>> GetListOrderFull(string dbName, bool thisMonth);
        Task<OrderFullResponse> GetOrderFull(string dbName, int id);
    }
}
