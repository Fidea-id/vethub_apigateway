namespace Domain.Entities.Responses.Clients
{
    public class PatientsListResponse
    {
        public int Id { get; set; }
        public int OwnersId { get; set; }
        public string OwnersName { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public string Color { get; set; }
        public bool IsAlive { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Vaccinated { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
