using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Filters.Clients
{
    public class ProductCategoriesFilter: BaseEntityFilter
    {
        public string? Name { get; set; }
    }
}
