using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Clients
{
    public class DocsKematianRequest
    {
        public int MedicalRecordsId { get; set; }
        public string DiedReason { get; set; }
        public DateTime DiedTime { get; set; }
        public string DiedBuriedInfo { get; set; }
    }
}
