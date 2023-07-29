using Domain.Entities.Models.Clients;

namespace Domain.Entities.Filters.Clients
{
    public class ProductDiscountsFilter : BaseEntityFilter
    {
        public int? ProductId { get; set; }
        public string? Description { get; set; }
        public double? DiscountValue { get; set; }
        public string? DiscountType { get; set; } // Enum or separate table to indicate the type of discount
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
