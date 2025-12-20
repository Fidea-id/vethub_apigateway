using Domain.Entities.DTOs.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Responses.Clients
{
    public class DoctorPerformanceResponse
    {
        public int Year { get; set; }
        public List<DoctorPerformanceDto> Doctors { get; set; } = new();
    }

}
