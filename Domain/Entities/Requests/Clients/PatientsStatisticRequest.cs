using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Clients
{
    public class PatientsStatisticRequest
    {
        public int PatientId { get; set; }
        public int StaffId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
