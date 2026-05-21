using Domain.Entities;

namespace Domain.Entities.Filters.Clients
{
    public class FinancialTransactionsFilter : BaseEntityFilter
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ReferenceNo { get; set; }
        public string? TransactionType { get; set; }
    }
}
