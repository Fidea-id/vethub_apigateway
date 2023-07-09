using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Clients
{
    public class AppointmentsStatusRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Weight { get; set; }
    }
}
