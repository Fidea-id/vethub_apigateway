namespace Domain.Entities.Filters.Clients
{
    public class PatientsStatisticFilter : BaseEntityFilter
    {
        public int? PatientId { get; set; }
        public string? Type { get; set; }
        public string? Unit { get; set; }
        public string? Value { get; set; }
    }
}
