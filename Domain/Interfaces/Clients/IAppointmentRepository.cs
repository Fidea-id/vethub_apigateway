using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IAppointmentRepository : IGenericRepository<Appointments, AppointmentsFilter>
    {
        //Task<IEnumerable<PatientsListResponse>> GetPatientsList(string dbName, AppointmentsFilter filter);
        Task AddStatusRange(IEnumerable<AppointmentsStatus> entities, string dbName);
        Task<IEnumerable<AppointmentsStatus>> GetAllStatus(string dbName);
        Task<IEnumerable<AppointmentsDetailResponse>> GetAllDetailList(string dbName);
        Task<AppointmentsDetailResponse> GetAllDetail(int id, string dbName);
    }
}
