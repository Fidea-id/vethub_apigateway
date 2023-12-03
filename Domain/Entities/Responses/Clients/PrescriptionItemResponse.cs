namespace Domain.Entities.Responses.Clients
{
    public class PrescriptionItemResponse : BaseEntity
    {
        public int Id { get;set; }
        public string Name { get;set; }
        public string Description { get;set; }
        public int stock { get;set; }
        public string Volume { get; set; }
        public string VolumeUnit { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
    }
}
