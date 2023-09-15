namespace Domain.Entities.Requests.Clients
{
    public class NotificationsRequest
    {
        public NotificationsRequest(int profileId, string type, string message, string detail, string url)
        {
            ProfileId = profileId; Type = type; Message = message; Detail = detail; URL = url;
        }
        public int? ProfileId { get; set; }
        public string? Type { get; set; }
        public string? Message { get; set; }
        public string? Detail { get; set; }
        public string? URL { get; set; }
        public bool? IsRead { get; set; }
    }
}
