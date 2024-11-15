﻿using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("ProductStocks")]
    public class ProductStocksXPO : XPLiteObject
    {
        public ProductStocksXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double Stock { get; set; }
        public double Volume { get; set; }
        public string VolumeUnit { get; set; }
        public double VolumeRemaining { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
