using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.Masters
{
    public class ClinicProductUsage : BaseEntity
    {
        public int? ClinicId { get; set; } //ambil dari tabel Clinics.Id di master (jika ada)
        public string? ClinicName { get; set; }
        public string Entity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public string? ProductAliasName { get; set; }
        public string? ProductCategory { get; set; }
        public string? ProductVolume { get; set; }
        public double Price { get; set; }
        public double TotalUsage { get; set; }
    }
}
