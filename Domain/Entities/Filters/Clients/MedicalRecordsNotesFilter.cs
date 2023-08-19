namespace Domain.Entities.Filters.Clients
{
    public class MedicalRecordsNotesFilter : BaseEntityFilter
    {
        public int? MedicalRecordsId { get; set; }
        public string? Type { get; set; }
        public string? Title { get; set; }
        public string? Value { get; set; }
    }
}
