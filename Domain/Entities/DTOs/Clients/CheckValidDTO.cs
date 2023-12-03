using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTOs.Clients
{
    public class CheckValidDTO
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public List<string> ValidationMessage { get; set; }
    }
}
