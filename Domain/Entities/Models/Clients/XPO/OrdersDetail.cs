﻿using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("OrdersDetail")]
    public class OrdersDetailXPO : XPLiteObject
    {
        public OrdersDetailXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int StaffId { get; set; }
        public int Quantity { get; set; }
        public double? Discount { get; set; }
        public string? DiscountType { get; set; }
        public double TotalPrice { get; set; }
    }
}
