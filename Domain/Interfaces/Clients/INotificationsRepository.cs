using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface INotificationsRepository : IGenericRepository<Notifications, NotificationsFilter>
    {
        Task<IEnumerable<Notifications>> TakeRecent(string dbName, int profileId);
        Task<IEnumerable<Notifications>> TakeAllDesc(string dbName, int profileId);
    }
}
