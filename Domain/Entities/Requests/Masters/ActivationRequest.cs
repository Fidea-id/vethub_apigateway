namespace Domain.Entities.Requests.Masters
{
    public class ActivationRequest
    {
        public string User { get; set; }
        public string Code { get; set; }
        public string? Password { get; set; }
    }
}
