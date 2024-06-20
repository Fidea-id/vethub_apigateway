namespace Domain.Entities.Models.Clients
{
    public class EventLogs : BaseEntity
    {
        public int RecordId { get; set; }
        public int UserId { get; set; } //user yang melakukan perubahan/record
        public string MethodName { get; set; }
        public string MethodType { get; set; }
        public string ObjectName { get; set; }
        public string Detail { get; set; }
    }
}
