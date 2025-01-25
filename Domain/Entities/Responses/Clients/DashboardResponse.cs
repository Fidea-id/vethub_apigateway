namespace Domain.Entities.Responses.Clients
{
    public class DashboardResponse
    {
        public CardDashboard Clients { get; set; }
        public CardDashboard Pets { get; set; }
        public CardDashboard Appointments { get; set; }
        public CardDashboard Products { get; set; }
        public CardDashboard Services { get; set; }
        public CardDashboard SalesInvoices { get; set; }
        public DoubleCardDashboard Revenues { get; set; }
        public DoubleCardDashboard RevenuesProducts { get; set; }
        public DoubleCardDashboard RevenuesServices { get; set; }
        public List<ActivityDashboard> Activities { get; set; }
        public List<ChartDataSeries> ChartClients { get; set; }
        public List<ChartDataSeries> ChartSalesGrowth { get; set; }
        public List<ChartDataSeries> ChartClientsGrowth { get; set; }
        public List<ChartDataSeries> ChartPatientType { get; set; }
        public List<ChartHeatmapSeries> WeeklyClient { get; set; }
        public List<FrequentDiagnoseMeds> FrequentDiagnoseMeds { get; set; }

        public string DateFilter { get; set; }
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

    public class ChartHeatmapSeries
    {
        public string Name { get; set; }
        public IEnumerable<DataPoint> Data { get; set; }
    }
    public class FrequentDiagnoseMeds
    {
        public string Name { get; set; } //name diagnose or products
        public string Type { get; set; } //type diagnose or products
        public int Total { get; set; } // total 
    }

    public class DataPoint
    {
        public int W { get; set; } // Numeric representation of day (1–7)
        public int X { get; set; }      // Hour (0–23)
        public int Y { get; set; } // Frequency of appointments
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
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public int Total { get; set; }
    }
}
