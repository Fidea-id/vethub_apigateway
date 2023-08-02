using DevExpress.Xpo;
using Domain.Entities.Attributes;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("Owners")]
    public class OwnersXPO : XPLiteObject
    {
        public OwnersXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public string Name { get; set; }
        [DbType("LONGTEXT")]
        public string Photo { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        [DbType("LONGTEXT")]
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
