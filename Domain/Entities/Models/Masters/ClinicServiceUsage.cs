using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.Masters
{
    public class ClinicServiceUsage : BaseEntity
    {
        public int? ClinicId { get; set; } //ambil dari tabel Clinics.Id di master (jika ada)
        public string? ClinicName { get; set; }
        public string Entity { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = "";
        public string DurationText { get; set; } = "";
        public double Price { get; set; }
        public double TotalUsage { get; set; }
    }
}
