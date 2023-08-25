using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("PrescriptionFrequents")]
    public class PrescriptionFrequentsXPO : XPLiteObject
    {
        public PrescriptionFrequentsXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lang { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
