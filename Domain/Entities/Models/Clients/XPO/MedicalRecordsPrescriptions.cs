using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("MedicalRecordsPrescriptions")]
    public class MedicalRecordsPrescriptionsXPO : XPLiteObject
    {
        public MedicalRecordsPrescriptionsXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int MedicalRecordsId { get; set; }
        public string ProductName { get; set; }
        public string PrescriptionFrequency { get; set; }
        public double PrescriptionAmount { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double Total { get; set; }
    }
}
