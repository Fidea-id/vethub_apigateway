namespace Domain.Entities.Filters.Masters
{
    public class UsersFilter : BaseEntityFilter
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Entity { get; set; }
        public string? Roles { get; set; }
        public bool? IsVerified { get; set; }
        public string? KeyChipper { get; set; }
        public string? PasswordToken { get; set; }
    }
}
