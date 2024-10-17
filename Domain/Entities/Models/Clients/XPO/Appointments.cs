using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("Appointments")]
    public class AppointmentsXPO : XPLiteObject
    {
        public AppointmentsXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public int OwnersId { get; set; }
        public int PatientsId { get; set; }
        public DateTime Date { get; set; }
        public int StaffId { get; set; }
        public int ServiceId { get; set; }
        public string Type { get; set; }
        public int StatusId { get; set; }
        [DbType("LONGTEXT")]
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
