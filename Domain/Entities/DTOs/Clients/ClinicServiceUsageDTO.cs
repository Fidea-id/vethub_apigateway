using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTOs.Clients
{
    public class ClinicServiceUsageDTO
    {
        public string Entity { get; set; }
        public string ClinicName { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string DurationText { get; set; }
        public double Price { get; set; }
        public int TotalUsage { get; set; }
    }
}
