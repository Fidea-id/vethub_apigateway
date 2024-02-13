namespace Domain.Entities.Responses.Masters
{
    public class UserDataResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Entity { get; set; }
    }
}
