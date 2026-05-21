using Domain.Entities;

namespace Domain.Entities.Filters.Clients
{
    public class JournalEntriesFilter : BaseEntityFilter
    {
        public int? TransactionId { get; set; }
        public int? ChartOfAccountId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
