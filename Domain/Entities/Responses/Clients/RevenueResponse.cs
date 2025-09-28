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
        public double? Discount { get; set; }
        public double? TotalAfterDiscount { get; set; }
        public string Details { get; set; }
        public string ClientName { get; set; }
        public string PaymentMethod { get; set; }
    }
    public class RevenueDataResponse
    {
        public double ServicesTotal { get; set; }
        public double ProductsTotal { get; set; }
        public double TotalDiscount { get; set; }
        public double Total { get; set; }
    }
}
