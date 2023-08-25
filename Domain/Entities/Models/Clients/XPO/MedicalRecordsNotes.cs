using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("MedicalRecordsNotes")]
    public class MedicalRecordsNotesXPO : XPLiteObject
    {
        public MedicalRecordsNotesXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int MedicalRecordsId { get; set; }
        public int StaffId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
