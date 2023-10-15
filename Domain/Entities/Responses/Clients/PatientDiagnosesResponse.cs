using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Responses.Clients
{
    public class PatientDiagnosesResponse
    {
        public string Diagnose { get; set; }
        public int Count { get; set; }
        public double Percentage { get; set; }
    }
}
