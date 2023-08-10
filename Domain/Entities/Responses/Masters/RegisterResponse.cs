namespace Domain.Entities.Responses.Masters
{
    public class RegisterResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Entity { get; set; }
        public string Roles { get; set; }
        public string ConfirmationToken { get; set; }
        //public int CLinicId { get; set; }
    }
}
