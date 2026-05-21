namespace Domain.Entities.Responses.Clients
{
    public class FinancialTransactionsResponse
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ReferenceNo { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public double TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<JournalEntriesResponse> Entries { get; set; } = new();
    }
}
