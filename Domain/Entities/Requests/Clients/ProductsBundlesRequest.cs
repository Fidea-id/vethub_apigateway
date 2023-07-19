using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Clients
{
    public class ProductsBundlesRequest
    {
        public int ProductId { get; set; }
        public double Quantity { get; set; }
    }
}
