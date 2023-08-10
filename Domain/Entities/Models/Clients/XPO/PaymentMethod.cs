using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("PaymentMethod")]
    public class PaymentMethodXPO : XPLiteObject
    {
        public PaymentMethodXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
