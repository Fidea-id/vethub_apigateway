using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("MedicalRecords")]
    public class MedicalRecordsXPO : XPLiteObject
    {
        public MedicalRecordsXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        [DbType("LONGTEXT")]
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string Code { get; set; }
        public int AppointmentId { get; set; }
        public int StaffId { get; set; }
        public int PatientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PaymentStatus { get; set; }
        public double Total { get; set; }
    }
}
