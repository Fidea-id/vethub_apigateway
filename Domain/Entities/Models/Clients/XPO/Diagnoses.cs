using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("Diagnoses")]
    public class DiagnosesXPO : XPLiteObject
    {
        public DiagnosesXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public string Name { get; set; }
        [DbType("LONGTEXT")]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
