namespace Domain.Entities.Models.Clients
{
    public class PaymentMethod : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
