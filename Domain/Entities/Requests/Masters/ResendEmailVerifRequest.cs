
namespace Domain.Entities.Requests.Masters
{
    public class ResendEmailVerifRequest
    {
        public Models.Masters.Users UserData { get; set; }
        public string ClinicName { get; set; }
    }
}
