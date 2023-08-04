using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Responses.Clients
{
    public class PatientsStatisticHistoryResponse
    {
        public string Value { get; set; }
        public DateTime Date { get; set; }
        public string CheckedBy { get; set; }
    }
}
