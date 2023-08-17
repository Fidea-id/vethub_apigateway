using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Responses.Clients
{
    public class DocGenerateResponse
    {
        public string Filename { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
}
