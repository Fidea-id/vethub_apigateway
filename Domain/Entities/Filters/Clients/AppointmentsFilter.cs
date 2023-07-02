using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Filters.Clients
{
    public class AppointmentsFilter : BaseEntityFilter
    {
        public int? OwnersId { get; set; }
        public int? PatientsId { get; set; }
        public DateTime? Date { get; set; }
        public int? StaffId { get; set; }
        public int? ServiceId { get; set; }
        public int? StatusId { get; set; }
        public string? Notes { get; set; }
    }
}
