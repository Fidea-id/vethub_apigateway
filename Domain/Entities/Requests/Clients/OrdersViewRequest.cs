using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Clients
{
    public class OrdersViewRequest
    {
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public string Client { get; set; }
        public string OrderType { get; set; }
        public IEnumerable<OrdersViewItemRequest> OrderItem { get; set; }
    }
    public class OrdersViewItemRequest
    {
        public string ItemId { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string Discount { get; set; }
        public string Total { get; set; }
    }
}
