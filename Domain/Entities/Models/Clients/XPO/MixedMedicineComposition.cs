using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("MixedMedicineComposition")]
    public class MixedMedicineCompositionXPO : XPLiteObject
    {
        public MixedMedicineCompositionXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int MixedMedicineId { get; set; }
        public int ProductId { get; set; }
        public double QuantityPerUnit { get; set; } // Amount of Product needed per 1 Mixed Medicine unit
    }
}
