namespace Domain.Entities.Requests
{
    public class PatientsRequest
    {
        public string? Name { get; set; }
        public string? Species { get; set; }
        public string? Breed { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? Vaccinated { get; set; }
    }
}
