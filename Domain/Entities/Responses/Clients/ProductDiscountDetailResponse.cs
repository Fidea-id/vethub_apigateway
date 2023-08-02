namespace Domain.Entities.Responses.Clients
{
    public class ProductDiscountDetailResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string DiscountType { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
    }
}
