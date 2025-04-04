
namespace Domain.Entities.Requests.Masters
{
    public class ResendEmailVerifRequest
    {
        public int Id { get;set; }
        //public Models.Masters.Users? UserData { get; set; }
        public string ClinicName { get; set; }
    }
}
