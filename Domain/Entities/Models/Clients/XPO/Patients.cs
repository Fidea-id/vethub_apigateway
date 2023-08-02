using DevExpress.Xpo;
using Domain.Entities.Attributes;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("Patients")]
    public class PatientsXPO : XPLiteObject
    {
        public PatientsXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public int OwnersId { get; set; }
        public string Name { get; set; }
        [DbType("LONGTEXT")]
        public string Photo { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Vaccinated { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
