using DevExpress.Xpo;
using Domain.Entities.Attributes;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("AppointmentsStatus")]
    public class AppointmentsStatusXPO : XPLiteObject
    {
        public AppointmentsStatusXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public string Name { get; set; }
        [DbType("LONGTEXT")]
        public string Description { get; set; }
        public int Weight { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
