using Domain.Entities.Attributes;

namespace Domain.Entities.Models.Clients
{
    public class AppointmentsStatus : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
    }
}
