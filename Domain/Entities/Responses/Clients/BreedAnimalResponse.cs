using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Responses.Clients
{
    public class BreedAnimalResponse
    {
        public int Id { get; set; }
        public int AnimalsId { get; set; }
        public string AnimalName { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
