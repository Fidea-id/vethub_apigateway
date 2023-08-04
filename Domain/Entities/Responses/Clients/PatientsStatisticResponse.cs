using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Responses.Clients
{
    public class PatientsStatisticResponse
    {
        public int PatientId { get; set; }
        public string Type { get; set; }
        public string Latest { get; set; }
        public string Before { get; set; }
        public string Change { get; set; }
        public string CheckedBy { get; set; }
        public string CheckedAt { get; set; }
    }
}
