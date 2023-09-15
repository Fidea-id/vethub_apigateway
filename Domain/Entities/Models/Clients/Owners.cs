using Domain.Entities.Attributes;

namespace Domain.Entities.Models.Clients
{
    public class Owners : BaseEntity
    {
        public Owners()
        {
        }
        public Owners(string name)
        {
            Name = name;
        }
        [MaxLength(100)]
        public string Name { get; set; }
        public string Photo { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(10)]
        public string Title { get; set; }
        public string Address { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
    }
}
