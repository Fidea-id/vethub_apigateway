using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Responses.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
