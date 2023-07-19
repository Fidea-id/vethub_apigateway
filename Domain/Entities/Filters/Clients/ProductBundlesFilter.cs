using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Filters.Clients
{
    public class ProductBundlesFilter: BaseEntityFilter
    {
        public int? ProductId { get; set; }
        public double? Quantity { get; set; }
    }
}
