using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Filters
{
    public class NameBaseEntityFilter: BaseEntityFilter
    {
        public string? Name { get; set; }
    }
}
