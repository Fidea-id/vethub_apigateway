using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Responses.Masters
{
    public class UserBillResponse
    {
        public int UserId { get; set; }
        public int SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public string Period { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
        public double Total { get; set; }
        public int MaxUser { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
