namespace Domain.Entities.Responses.Clients
{
    public class ProductResponse : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
        public bool IsBundle { get; set; }
        public bool HasDiscount { get; set; }
    }
}
