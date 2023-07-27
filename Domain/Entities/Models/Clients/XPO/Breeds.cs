using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("Breeds")]
    public class BreedsXPO : XPLiteObject
    {
        public BreedsXPO(Session session) : base(session) { }

        [Key]
        public int Id { get; set; }
        public int AnimalsId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
