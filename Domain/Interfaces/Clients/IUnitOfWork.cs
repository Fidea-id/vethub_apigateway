﻿namespace Domain.Interfaces.Clients
{
    public interface IUnitOfWork : IDisposable
    {
        //tambahkan interface repository disini
        IProfileRepository ProfileRepository { get; }
        IServicesRepository ServicesRepository { get; }
        IOwnersRepository OwnersRepository { get; }
        IPatientsRepository PatientsRepository { get; }
        IProductsRepository ProductsRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }
    }
}
