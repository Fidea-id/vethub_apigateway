using Domain.Entities.Filters.Masters;
using Domain.Entities.Models.Masters;

namespace Domain.Interfaces.Masters
{
    public interface IBillPaymentsRepository : IGenericRepository<BillPayments, BillPaymentsFilter>
    {
        Task<IEnumerable<BillStatus>> GetStatusBills();
        Task<BillStatus> GetStatusBill(int id);
    }
}
