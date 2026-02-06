namespace Domain.Entities.DTOs.Clients
{
    public class ClinicProductUsageDTO
    {
        public string Entity { get; set; }
        public string ClinicName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public string? ProductAliasName { get; set; }
        public string? ProductCategory { get; set; }
        public string? ProductVolume { get; set; }
        public double Price { get; set; }
        public double TotalUsage { get; set; }
    }
}
