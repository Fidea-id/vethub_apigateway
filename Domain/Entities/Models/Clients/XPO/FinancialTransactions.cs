using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("FinancialTransactions")]
    public class FinancialTransactionsXPO : XPLiteObject
    {
        public FinancialTransactionsXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string VoucherNo { get; set; }
        public string ReferenceNo { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string TransactionType { get; set; } = string.Empty; // General, Income, Expense, etc.
        public double TotalAmount { get; set; }
        public string Status { get; set; } = "Posted";
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
