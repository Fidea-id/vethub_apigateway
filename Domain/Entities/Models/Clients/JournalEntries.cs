using Domain.Entities;

namespace Domain.Entities.Models.Clients
{
    public class JournalEntries : BaseEntity
    {
        public int TransactionId { get; set; }
        public int ChartOfAccountId { get; set; }
        public string? Description { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
    }
}
