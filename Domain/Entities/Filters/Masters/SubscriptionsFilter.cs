namespace Domain.Entities.Filters.Masters
{
    public class SubscriptionsFilter : BaseEntityFilter
    {
        public string? Name { get; set; }
        public string? PeriodeType { get; set; }
        public double? Price { get; set; }
    }
}
