namespace Domain.Entities.Filters.Clients
{
    public class MedicalRecordsPrescriptionsFilter : BaseEntityFilter
    {
        public int? MedicalRecordsId { get; set; }
        public int? ProductId { get; set; }
        public string? PrescriptionFrequency { get; set; }
        public string? Type { get; set; }
        public double? PrescriptionAmount { get; set; }
        public double? Price { get; set; }
        public double? Quantity { get; set; }
        public double? Total { get; set; }
    }
}
