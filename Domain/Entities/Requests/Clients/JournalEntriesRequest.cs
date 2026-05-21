namespace Domain.Entities.Requests.Clients
{
    public class JournalEntriesRequest
    {
        public int Id { get; set; }
        public int? TransactionId { get; set; }
        public int ChartOfAccountId { get; set; }
        public string? Description { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
    }
}
