using Domain.Entities.Attributes;

namespace Domain.Entities.Models.Masters
{
    public class Users : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Entity { get; set; }
        [MaxLength(20)]
        public string Roles { get; set; }
        public bool IsVerified { get; set; }
        public string? KeyChipper { get; set; }
        public string? PasswordToken { get; set; }
    }
}
