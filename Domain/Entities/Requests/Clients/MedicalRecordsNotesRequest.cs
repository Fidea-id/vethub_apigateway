using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Clients
{
    public class MedicalRecordsNotesRequest
    {
        public int MedicalRecordsId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
