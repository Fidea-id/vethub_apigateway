
namespace Domain.Entities.Requests.Clients
{
    public class OwnersPetsRequest
    {
        public OwnersAddRequest OwnersData { get; set; }
        public IEnumerable<PetsAddRequest> PetsData { get; set; }
    }
    public class OwnersAddRequest
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
    public class PetsAddRequest
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool Vaccinated { get; set; }
    }
}
