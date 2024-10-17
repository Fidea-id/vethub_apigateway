using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("Clinics")]
    public class ClinicsXPO : XPLiteObject
    {
        public ClinicsXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        [DbType("LONGTEXT")]
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string? Description { get; set; }
        public string PhoneNumber { get; set; }
        public string? WebUrl { get; set; }
        public string? MapUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
