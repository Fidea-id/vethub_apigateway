namespace Domain.Entities.Requests.Masters
{
    public class UserRegisterRequest
    {
        public string Email { get; set; }
        public string? Entity { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}
