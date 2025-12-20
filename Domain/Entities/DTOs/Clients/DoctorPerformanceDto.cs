using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTOs.Clients
{
    public class DoctorPerformanceDto
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public Dictionary<int, int> Monthly { get; set; } = new();
    }
    public class DoctorPerformanceRawDto //get from db raw
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int Month { get; set; }
        public int Total { get; set; }
    }

    public class DoctorPerformanceMonthlyDto
    {
        public int Month { get; set; }
        public int Total { get; set; }
    }

}
