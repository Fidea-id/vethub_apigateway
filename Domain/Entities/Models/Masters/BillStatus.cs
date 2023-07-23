using Domain.Entities.Attributes;

namespace Domain.Entities.Models.Masters
{
    public class BillStatus : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
