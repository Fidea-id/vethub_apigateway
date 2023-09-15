using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{

    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("Notifications")]
    public class NotificationsXPO : XPLiteObject
    {
        public NotificationsXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public string URL { get; set; }
        public bool IsRead { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
