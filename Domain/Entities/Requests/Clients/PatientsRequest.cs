namespace Domain.Entities.Requests.Clients
{
    public class PatientsRequest
    {
        public int? OwnersId { get; set; }
        public string? Name { get; set; }
        public string? Photo { get; set; }
        public string? Species { get; set; }
        public string? Breed { get; set; }
        public string? Gender { get; set; }
        public string? Color { get; set; }
        public bool IsAlive { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool Vaccinated { get; set; }
    }
}
