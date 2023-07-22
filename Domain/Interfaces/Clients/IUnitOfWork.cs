namespace Domain.Interfaces.Clients
{
    public interface IUnitOfWork : IDisposable
    {
        //tambahkan interface repository disini
        IProfileRepository ProfileRepository { get; }
        IGenerateTableRepository GenerateTableRepository { get; }
        IServicesRepository ServicesRepository { get; }
        IOwnersRepository OwnersRepository { get; }
        IPatientsRepository PatientsRepository { get; }
        IProductsRepository ProductsRepository { get; }
        IProductBundlesRepository ProductBundlesRepository { get; }
        IProductCategoriesRepository ProductCategoriesRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }
        IAnimalRepository AnimalRepository { get; }
        IBreedRepository BreedRepository { get; }
    }
}
