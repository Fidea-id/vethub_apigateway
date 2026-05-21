namespace Domain.Entities.Responses.Clients
{
    public class JournalEntriesResponse
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int ChartOfAccountId { get; set; }
        public string AccountCode { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
    }
}
