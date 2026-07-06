using System;

namespace Domain.Entities.Requests.Clients
{
    public class ExpenseRequest
    {
        public int ExpenseAccountId { get; set; }
        public int PaymentAccountId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? TransactionType { get; set; }
    }
}
