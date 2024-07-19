using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("ClinicConfig")]
    public class ClinicConfigXPO : XPLiteObject
    {
        public ClinicConfigXPO(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
