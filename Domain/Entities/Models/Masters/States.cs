using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Models.Masters
{
    public class States
    {
        [Key]
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public string Name { get; set; }
    }
}
