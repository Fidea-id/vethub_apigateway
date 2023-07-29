namespace Domain.Entities.Models.Clients
{
    public class ProductDiscounts : BaseEntity
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public double DiscountValue { get; set; }
        public string DiscountType { get; set; } // Enum or separate table to indicate the type of discount
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public enum DiscountType
    {
        Percentage,
        Amount,
    }
}
