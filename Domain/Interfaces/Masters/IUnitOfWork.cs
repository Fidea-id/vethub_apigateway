namespace Domain.Interfaces.Masters
{
    public interface IUnitOfWork : IDisposable
    {
        //tambahkan interface repository disini
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
        Task<int> SaveChanges();
        IUserRepository UserRepository { get; }
        IBillPaymentsRepository BillPaymentsRepository { get; }
        IDocsTypeRepository DocsTypeRepository { get; }
        IRolesRepository RolesRepository { get; }
        ISubscriptionsRepository SubscriptionsRepository { get; }
    }
}
