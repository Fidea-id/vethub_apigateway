using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Clients
{
    public class BreedsRequest
    {
        public int AnimalsId { get; set; }
        public string Name { get; set; }
    }
}
