using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTOs.Clients
{
    public class PatientsStatisticDto
    {
        public int PatientId { get; set; }
        public int StaffId { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
        public string Latest { get; set; }
        public string Before { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
