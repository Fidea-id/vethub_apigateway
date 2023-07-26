namespace Domain.Entities.Models.Clients
{
    public class Products : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public bool IsBundle { get; set; } // Indicates if the product is a bundle
    }
}
