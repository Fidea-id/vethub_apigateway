namespace Domain.Entities.Requests.Clients
{
    public class FinancialTransactionsRequest
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? ReferenceNo { get; set; }
        public string? Description { get; set; }
        public string TransactionType { get; set; } = "General";
        public double TotalAmount { get; set; }
        public List<JournalEntriesRequest> Entries { get; set; } = new();
    }
}
