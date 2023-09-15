
namespace Domain.Entities.Filters.Clients
{
    public class NotificationsFilter : BaseEntityFilter
    {
        public int? ProfileId { get; set; }
        public string? Type { get; set; }
        public string? Message { get; set; }
        public string? Detail { get; set; }
        public string? URL { get; set; }
        public bool? IsRead { get; set; }
    }
}
