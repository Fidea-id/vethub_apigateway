using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("OpnamePatients")]
    public class OpnamePatientsXPO : XPLiteObject
    {
        public OpnamePatientsXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public int MedicalRecordId { get; set; }
        public int OpnameId { get; set; }
        public DateTime StartTime { get; set; }
        public int EstimateDays { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
