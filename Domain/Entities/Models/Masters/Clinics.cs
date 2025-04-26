using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.Masters
{
    public class Clinics : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string Entity { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string? Description { get; set; }
        public string PhoneNumber { get; set; }
    }
}
