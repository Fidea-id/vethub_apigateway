using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Responses.Clients
{
    public class ResponseUploadBulk
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<string> validationMessage { get; set; }
    }
}
