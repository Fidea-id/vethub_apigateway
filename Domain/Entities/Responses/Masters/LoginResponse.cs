namespace Domain.Entities.Responses.Masters
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Roles { get; set; }
        public string SessionToken { get; set; }
    }
}
