﻿namespace Domain.Entities.Requests.Clients
{
    public class ProductsDiscountsRequest
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public double DiscountValue { get; set; }
        public string DiscountType { get; set; } // Enum or separate table to indicate the type of discount
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
