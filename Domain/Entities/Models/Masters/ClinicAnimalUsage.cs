using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.Masters
{
    public class ClinicAnimalUsage : BaseEntity
    {
        public int? ClinicId { get; set; } //ambil dari tabel Clinics.Id di master (jika ada)
        public string? ClinicName { get; set; }
        public string Entity { get; set; }
        public int AnimalId { get; set; }
        public string Name { get; set; } = "";
        public int Count { get; set; }
    }
}
