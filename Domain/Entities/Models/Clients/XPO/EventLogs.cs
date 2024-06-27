using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("EventLogs")]
    public class EventLogsXPO : XPLiteObject
    {
        public EventLogsXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public int RecordId { get; set; }
        public int UserId { get; set; }
        public string MethodName { get; set; }
        public string MethodType { get; set; }
        public string ObjectName { get; set; }
        [DbType("LONGTEXT")]
        public string Detail { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
