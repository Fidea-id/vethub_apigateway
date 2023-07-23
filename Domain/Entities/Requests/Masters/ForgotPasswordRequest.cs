namespace Domain.Entities.Requests.Masters
{
    public class ForgotPasswordRequest
    {
        public string? User { get; set; }
        public string? Email { get; set; }
        public string? Code { get; set; }
        public string? NewPass { get; set; }
        public string? OldPass { get; set; }
    }
}
