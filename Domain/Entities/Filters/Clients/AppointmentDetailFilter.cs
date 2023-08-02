using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Filters.Clients
{
    public class AppointmentDetailFilter:BaseEntityFilter
    {
        public int? StatusId { get; set; }
        //public int? BranchId { get; set; }
        public int? StaffId { get; set; }
        public string? Date { get; set; }
    }
}
