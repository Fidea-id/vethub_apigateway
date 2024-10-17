using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Models.Masters
{
    public class Provinces
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
