namespace Domain.Entities.Responses.Clients
{
    public class RevenueResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public double Total { get; set; }
        public string Details { get; set; }
    }
}
