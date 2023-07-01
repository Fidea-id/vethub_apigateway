using Domain.Entities.Attributes;

namespace Domain.Entities.Models.Clients
{
    public class Patients : BaseEntity
    {
        public int OwnersId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string Photo { get; set; }
        [MaxLength(25)]
        public string Species { get; set; }
        [MaxLength(50)]
        public string Breed { get; set; }
        [MaxLength(10)]
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Vaccinated { get; set; }
    }
}
