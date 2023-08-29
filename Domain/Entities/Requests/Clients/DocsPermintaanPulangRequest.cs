using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Clients
{
    public class DocsPermintaanPulangRequest
    {
        public int MedicalRecordsId { get; set; }
        public string OwnerIdNumber { get; set; }
    }
}
