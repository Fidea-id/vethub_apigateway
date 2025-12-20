namespace Domain.Entities.Filters.Masters
{
    public class ClinicsFilter : BaseEntityFilter
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Entity { get; set; }
    }
}
