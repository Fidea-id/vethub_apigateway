namespace Domain.Entities.Requests
{
    public class ServicesRequest
    {
        public string? Name { get; set; }
        public int? Duration { get; set; }
        public string? DurationType { get; set; }
        public bool? IsActive { get; set; }
        public int? Price { get; set; }
    }
}
