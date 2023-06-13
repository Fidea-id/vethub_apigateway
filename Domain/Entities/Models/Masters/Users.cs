namespace Domain.Entities.Models.Masters
{
    public class Users : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Entity { get; set; }
        public string Roles { get; set; }
        public bool IsVerified { get; set; }
        public string? KeyChipper { get; set; }
        public string? PasswordToken { get; set; }
    }
}
