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
        public IEnumerable<MedicalRecordsDiagnosesRequest> Diagnoses { get; set; }
        public IEnumerable<MedicalRecordsPrescriptionsRequest> Prescriptions { get; set; }
    }
    public class MedicalRecordsDiagnosesRequest
    {
        public string Diagnose { get; set; }
        public string Action { get; set; }
        public double TotalPrice { get; set; }
    }
}
