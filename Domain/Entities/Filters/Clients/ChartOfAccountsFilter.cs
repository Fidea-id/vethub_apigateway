using Domain.Entities;

namespace Domain.Entities.Filters.Clients
{
    public class ChartOfAccountsFilter : BaseEntityFilter
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? SubType { get; set; }
        public int? ParentId { get; set; }
    }
}
