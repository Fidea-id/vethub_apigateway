using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Clients
{
    public class GeneratePDFRequest
    {
    }
    public class GenerateInvoicePdfRequest
    {
        public string data { get; set; }
        public bool needHistory { get; set; }
    }
}
