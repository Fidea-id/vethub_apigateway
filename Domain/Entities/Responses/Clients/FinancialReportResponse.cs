namespace Domain.Entities.Responses.Clients
{
    public class FinancialReportResponse
    {
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<FinancialReportGroupResponse> Groups { get; set; } = new();
        public double NetTotal { get; set; }
    }

    public class FinancialReportGroupResponse
    {
        public string GroupName { get; set; } = string.Empty;
        public List<FinancialReportItemResponse> Items { get; set; } = new();
        public double SubTotal { get; set; }
    }

    public class FinancialReportItemResponse
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double Amount { get; set; }
    }
}
