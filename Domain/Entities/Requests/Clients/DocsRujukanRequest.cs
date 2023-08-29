using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Clients
{
    public class DocsRujukanRequest
    {
        public int MedicalRecordsId { get; set; }
        public string ClinicRefferalName { get; set; }
        public IEnumerable<string> MedicalAction { get; set; }
    }
}
