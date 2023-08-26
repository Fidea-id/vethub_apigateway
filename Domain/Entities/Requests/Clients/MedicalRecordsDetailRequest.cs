using Domain.Entities.Models.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Clients
{
    public class MedicalRecordsDetailRequest
    {
        public int MedicalRecordsId { get; set; }
        public IEnumerable<string> Diagnoses { get; set; }
        public IEnumerable<MedicalRecordsPrescriptionsRequest> Prescriptions { get; set; }
    }
}
