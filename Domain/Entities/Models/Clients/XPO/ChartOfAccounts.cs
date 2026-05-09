using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("ChartOfAccounts")]
    public class ChartOfAccountsXPO : XPLiteObject
    {
        public ChartOfAccountsXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public int? ParentId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
