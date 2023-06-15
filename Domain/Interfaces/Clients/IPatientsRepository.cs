﻿using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IPatientsRepository : IGenericRepository<Patients, PatientsFilter>
    {
        Task<IEnumerable<PatientsListResponse>> GetPatientsList(string dbName, PatientsFilter filter);
    }
}