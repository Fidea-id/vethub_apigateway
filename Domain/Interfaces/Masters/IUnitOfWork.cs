namespace Domain.Interfaces.Masters
{
    public interface IUnitOfWork : IDisposable
    {
        //tambahkan interface repository disini
        Task<int> Complete();
        IUserRepository UserRepository { get; }
        IBillPaymentsRepository BillPaymentsRepository { get; }
        IClinicsRepository ClinicsRepository { get; }
        IRolesRepository RolesRepository { get; }
        ISubscriptionsRepository SubscriptionsRepository { get; }
    }
}
