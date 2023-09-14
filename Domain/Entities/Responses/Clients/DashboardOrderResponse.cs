namespace Domain.Entities.Responses.Clients
{
    public class DashboardOrderResponse
    {
        public int IncomesTotal { get; set; }
        public int ExpensesTotal { get; set; }
        public double IncomesAmount { get; set; }
        public double ExpensesAmount { get; set; }
        public string IncomesAmountText { get; set; }
        public string ExpensesAmountText { get; set; }
    }
}
