using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("JournalEntries")]
    public class JournalEntriesXPO : XPLiteObject
    {
        public JournalEntriesXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int ChartOfAccountId { get; set; }
        public string? Description { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
