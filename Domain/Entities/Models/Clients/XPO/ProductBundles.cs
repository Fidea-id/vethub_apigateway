using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("ProductBundles")]
    public class ProductBundlesXPO : XPLiteObject
    {
        public ProductBundlesXPO(Session session) : base(session) { }

        [Key]
        public int Id { get; set; }
        public int BundleId { get; set; }
        public int ItemId { get; set; }
        public double Quantity { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
