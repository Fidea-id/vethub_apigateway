using Domain.Entities.Attributes;

namespace Domain.Entities.Models.Clients
{
    public class Profile : BaseEntity
    {
        public int GlobalId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string Entity { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        public string Photo { get; set; }
        [MaxLength(20)]
        public string Roles { get; set; }
    }
}
