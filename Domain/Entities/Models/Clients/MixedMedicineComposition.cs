namespace Domain.Entities.Models.Clients
{
    public class MixedMedicineComposition : BaseEntity
    {
        public int MixedMedicineId { get; set; }
        public int ProductId { get; set; }
        public double QuantityPerUnit { get; set; } // Amount of Product needed per 1 Mixed Medicine unit
    }
}
