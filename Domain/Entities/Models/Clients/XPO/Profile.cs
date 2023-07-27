using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("Profile")]
    public class ProfileXPO : XPLiteObject
    {
        public ProfileXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public int GlobalId { get; set; }
        public string Name { get; set; }
        [DbType("LONGTEXT")]
        public string Entity { get; set; }
        public string Email { get; set; }
        [DbType("LONGTEXT")]
        public string Photo { get; set; }
        public string Roles { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
