using Domain.Entities.Attributes;

namespace Domain.Entities.Models.Masters
{
    public class Clinics : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string State { get; set; }
        public string? Description { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        public string Entity { get; set; }
        public string? WebUrl { get; set; }
        public string? MapUrl { get; set; }
    }
}
