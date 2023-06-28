using Domain.Entities.Attributes;

namespace Domain.Entities.Models.Masters
{
    public class Roles : BaseEntity
    {
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
