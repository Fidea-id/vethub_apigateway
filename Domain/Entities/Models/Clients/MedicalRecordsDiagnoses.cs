using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.Clients
{
    public class MedicalRecordsDiagnoses : BaseEntity
    {
        public int MedicalRecordsId { get; set; }
        public string Diagnose { get; set; }
    }
}
