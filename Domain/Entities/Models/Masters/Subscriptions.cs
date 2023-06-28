using Domain.Entities.Attributes;

namespace Domain.Entities.Models.Masters
{
    public class Subscriptions : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string PeriodeType { get; set; }
        public double Price { get; set; }
    }
}
