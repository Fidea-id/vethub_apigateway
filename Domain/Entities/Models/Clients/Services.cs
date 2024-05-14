using Domain.Entities.Attributes;

namespace Domain.Entities.Models.Clients
{
    public class Services : BaseEntity
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        [MaxLength(20)]
        public string DurationType { get; set; }
        //public double Price { get; set; }
    }
}
