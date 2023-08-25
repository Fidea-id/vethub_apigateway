using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.Clients
{
    public class PrescriptionFrequents : BaseEntity
    {
        public string Name { get; set; }
        public string Lang { get; set; }
    }
}
