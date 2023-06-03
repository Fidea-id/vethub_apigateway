using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Requests
{
    public class UserLoginRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
