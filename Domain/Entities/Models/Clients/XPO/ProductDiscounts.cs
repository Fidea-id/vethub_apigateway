using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("ProductDiscounts")]
    public class ProductDiscountsXPO : XPLiteObject
    {
        public ProductDiscountsXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        [DbType("LONGTEXT")]
        public string Description { get; set; }
        public double DiscountValue { get; set; }
        public string DiscountType { get; set; } // Enum or separate table to indicate the type of discount
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
