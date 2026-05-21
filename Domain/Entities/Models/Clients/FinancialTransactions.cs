using Domain.Entities;

namespace Domain.Entities.Models.Clients
{
    public class FinancialTransactions : BaseEntity
    {
        public DateTime TransactionDate { get; set; }
        public string VoucherNo { get; set; }
        public string ReferenceNo { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string TransactionType { get; set; } = string.Empty; // General, Income, Expense, etc.
        public double TotalAmount { get; set; }
        public string Status { get; set; } = "Posted";
    }
}
