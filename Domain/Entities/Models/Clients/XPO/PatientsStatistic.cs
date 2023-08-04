using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("PatientsStatistic")]
    public class PatientsStatisticXPO : XPLiteObject
    {
        public PatientsStatisticXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int StaffId { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
