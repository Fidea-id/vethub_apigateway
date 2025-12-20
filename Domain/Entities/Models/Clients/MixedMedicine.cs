namespace Domain.Entities.Models.Clients
{
    public class MixedMedicine : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; } // E.g., "ml", "pills", etc.
    }
}
