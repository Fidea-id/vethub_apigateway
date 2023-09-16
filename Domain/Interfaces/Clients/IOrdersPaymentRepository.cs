using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IOrdersPaymentRepository : IGenericRepository<OrdersPayment, OrdersPaymentFilter>
    {
        Task<IEnumerable<OrdersPayment>> GetPaidByOrderId(string dbName, int orderId, string type);
    }
}
