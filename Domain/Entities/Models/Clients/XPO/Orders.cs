using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("Orders")]
    public class OrdersXPO : XPLiteObject
    {
        public OrdersXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int BranchId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int StaffId { get; set; }
        public int TotalQuantity { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public double TotalPrice { get; set; }
        public double TotalDiscountedPrice { get; set; }
    }
}
