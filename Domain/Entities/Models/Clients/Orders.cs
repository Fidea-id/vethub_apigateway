namespace Domain.Entities.Models.Clients
{
    public class Orders : BaseEntity
    {
        public int BranchId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int ClientId { get; set; }
        public int StaffId { get; set; }
        public string ClientName { get; set; }
        public int TotalQuantity { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public double TotalPrice { get; set; }
        public double TotalDiscountedPrice { get; set; }
        public double TotalDiscount { get; set; }
    }
}
