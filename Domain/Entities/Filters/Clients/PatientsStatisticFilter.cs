using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Filters.Clients
{
    public class PatientsStatisticFilter: BaseEntityFilter
    {
        public int? PatientId { get; set; }
        public string? Type { get; set; }
        public string? Unit { get; set; }
        public string? Value { get; set; }
    }
}
