namespace Domain.Entities.Responses.Clients
{
    public class DashboardResponse
    {
        public CardDashboard Clients { get; set; }
        public CardDashboard Pets { get; set; }
        public CardDashboard Appointments { get; set; }
        public DoubleCardDashboard Revenues { get; set; }

        public List<ActivityDashboard> Activities { get; set; }
        public List<ChartDataSeries> ChartClients { get; set; }

    }

    public class DashboardAdminResponse
    {
        public CardDashboard PracticeClients { get; set; }
        public CardDashboard ClinicClients { get; set; }
        public CardDashboard TotalUsers { get; set; }
        public CardDashboard TotalRevenue { get; set; }
        public List<ChartDataSeries> ChartClients { get; set; }

    }
    public class DoubleCardDashboard
    {
        public double TotalAll { get; set; }
        public double Total { get; set; }
        public double Percentage { get; set; }
    }

    public class CardDashboard
    {
        public int TotalAll { get; set; }
        public int Total { get; set; }
        public double Percentage { get; set; }
    }

    public class ChartDataSeries
    {
        public string Name { get; set; }
        public IEnumerable<MonthlyDataChart> Data { get; set; }
    }

    public class ActivityDashboard
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
    }
    public class MonthlyDataChart
    {
        public string Month { get; set; }
        public string Year { get; set; }
        public int Total { get; set; }
    }
}
