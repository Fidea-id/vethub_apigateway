namespace Domain.Entities.Models.Clients
{
    public class Opnames : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double BasePrice { get; set; }
    }
}
