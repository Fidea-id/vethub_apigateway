using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Clients
{
    public class DevExtremeFilterParams : BaseEntityFilter
    {
        public int? skip { get; set; }
        public int? take { get; set; }
        public string? sort { get; set; }
        public string? filter { get; set; }
        public bool? requireTotalCount { get; set; }
    }

}
