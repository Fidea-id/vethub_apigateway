using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTOs.Clients
{
    public class PrescriptionsItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Stock { get; set; }
        public string Volume { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
    }
}
