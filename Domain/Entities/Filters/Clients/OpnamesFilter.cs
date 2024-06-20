namespace Domain.Entities.Filters.Clients
{
    public class OpnamesFilter : BaseEntityFilter
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? BasePrice { get; set; }
    }
}
