using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Responses
{
    public class BaseValuesResponse
    {
        public IEnumerable<string> Value { get; set; }
    }
}
