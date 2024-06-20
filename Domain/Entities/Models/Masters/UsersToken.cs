namespace Domain.Entities.Models.Masters
{
    public class UsersToken : BaseEntity
    {
        public int UserId { get; set; }
        public string FCMToken { get; set; }
    }
}
