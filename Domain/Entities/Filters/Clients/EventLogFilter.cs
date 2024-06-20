namespace Domain.Entities.Filters.Clients
{
    public class EventLogFilter : BaseEntityFilter
    {
        public int? RecordId { get; set; }
        public int? UserId { get; set; }
        public string? MethodName { get; set; }
        public string? MethodType { get; set; }
        public string? ObjectName { get; set; }
        public string? Detail { get; set; }
    }
}
