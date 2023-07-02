﻿using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
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
    }
}
