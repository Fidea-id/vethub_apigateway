using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("MedicalRecordsDiagnoses")]
    public class MedicalRecordsDiagnosesXPO : XPLiteObject
    {
        public MedicalRecordsDiagnosesXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int MedicalRecordsId { get; set; }
        public string Diagnose { get; set; }
        public string Action { get; set; }
        public double TotalPrice { get; set; }
    }
}
