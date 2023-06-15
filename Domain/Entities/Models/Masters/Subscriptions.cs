namespace Domain.Entities.Models.Masters
{
    public class Subscriptions : BaseEntity
    {
        public string Name { get; set; }
        public string PeriodyType { get; set; }
        public int Price { get; set; }
    }
}
