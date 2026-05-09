namespace Domain.Entities.Requests.Clients
{
    public class ChartOfAccountsRequest
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? SubType { get; set; }
        public int? ParentId { get; set; }
    }
}
