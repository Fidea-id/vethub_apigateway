using Domain.Entities;

namespace Domain.Entities.Models.Clients
{
    public class ChartOfAccounts : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Asset, Liability, Equity, Revenue, Expense
        public string? SubType { get; set; } // Current, Fixed, etc.
        public int? ParentId { get; set; }
    }
}
