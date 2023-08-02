using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("AppointmentsActivity")]
    public class AppointmentsActivityXPO : XPLiteObject
    {
        public AppointmentsActivityXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int CurrentStatusId { get; set; }
        public DateTime CurrentDate { get; set; }
        [DbType("LONGTEXT")]
        public string Note { get; set; }
        public int StaffId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
