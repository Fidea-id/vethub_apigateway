namespace Domain.Entities.Filters.Masters
{
    public class ClinicsFilter : BaseEntityFilter
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Entity { get; set; }
    }
}
