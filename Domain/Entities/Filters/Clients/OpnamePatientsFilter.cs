namespace Domain.Entities.Filters.Clients
{
    public class OpnamePatientsFilter : BaseEntityFilter
    {
        public int? MedicalRecordId { get; set; }
        public int? OpnameId { get; set; }
        public DateTime? StartTime { get; set; }
        public int? EstimateDays { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }
        public double? Price { get; set; }
        public double? TotalPrice { get; set; }
    }
}
