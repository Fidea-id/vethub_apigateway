using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("Products")]
    public class ProductsXPO : XPLiteObject
    {
        public ProductsXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public string Name { get; set; }
        [DbType("LONGTEXT")]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public bool IsBundle { get; set; } // Indicates if the product is a bundle
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
