namespace Domain.Entities.Responses
{
    public class UserProfileResponse
    {
        public int Id { get; set; }
        public int GlobalId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string Roles { get; set; }
    }
}
