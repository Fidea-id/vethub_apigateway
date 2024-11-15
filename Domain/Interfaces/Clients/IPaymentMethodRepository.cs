﻿using Domain.Entities.Filters;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IPaymentMethodRepository : IGenericRepository<PaymentMethod, NameBaseEntityFilter>
    {
    }
}
