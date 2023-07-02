using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Responses.Clients
{
    public class AppointmentsDetailResponse
    {
        public int OwnersId { get; set; }
        public string OwnersName { get; set; }
        public int PatientsId { get; set; }
        public string PatientsName { get; set; }
        public DateTime Date { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string Notes { get; set; }
    }
}
