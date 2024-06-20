namespace Domain.Entities.DTOs.Clients
{
    public class PrescriptionsItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Stock { get; set; }
        public double Volume { get; set; }
        public string VolumeUnit { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
    }
}
