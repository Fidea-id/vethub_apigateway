using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Clients
{
    public class OrderFullRequest
    {
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int ClientId { get; set; }
        public int StaffId { get; set; }
        public string Type { get; set; }
        public string? Status { get; set; }
        //public int TotalQuantity { get; set; }
        //public double TotalPrice { get; set; }
        //public double? TotalDiscountedPrice { get; set; }
        public IEnumerable<OrderFullItem> OrderDetailItem { get; set; }
    }
    public class OrderFullItem
    {
        public int ProductId { get; set; }
        public int StaffId { get; set; }
        public int Quantity { get; set; }
        public double? Discount { get; set; }
        public string? DiscountType { get; set; }
        public double TotalPrice { get; set; }
    }
}
