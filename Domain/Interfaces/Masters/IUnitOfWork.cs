namespace Domain.Interfaces.Masters
{
    public interface IUnitOfWork : IDisposable
    {
        //tambahkan interface repository disini
        Task<int> Complete();
        IUserRepository UserRepository { get; }
    }
}
