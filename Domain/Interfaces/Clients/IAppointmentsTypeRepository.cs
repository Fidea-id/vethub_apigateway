﻿using Domain.Entities.Filters;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clients
{
    public interface IAppointmentsTypeRepository : IGenericRepository<AppointmentsType, NameBaseEntityFilter>
    {
    }
}
