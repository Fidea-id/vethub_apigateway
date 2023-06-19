namespace Domain.Entities.Filters.Masters
{
    public class SubscriptionsFilter : BaseEntityFilter
    {
        public string? Name { get; set; }
        public string? PeriodyType { get; set; }
        public int? Price { get; set; }
    }
}
