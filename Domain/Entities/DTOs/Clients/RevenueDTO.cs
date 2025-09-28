using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTOs.Clients
{
    public class RevenueDTO
    {
        public DateTime CreatedAt { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
        public double Total { get; set; }
        public double DiscountTotal { get; set; }
        public double TotalDiscounted { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string StatusPayment { get; set; }
    }
}
