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
        IProductStockRepository ProductStockRepository { get; }
        IProductBundlesRepository ProductBundlesRepository { get; }
        IProductCategoriesRepository ProductCategoriesRepository { get; }
        IProductDiscountsRepository ProductDiscountsRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }
        IDiagnosesRepository DiagnoseRepository { get; }
        IAnimalRepository AnimalRepository { get; }
        IBreedRepository BreedRepository { get; }
        IPatientsStatisticRepository PatientsStatisticRepository { get; }
        IOrdersRepository OrdersRepository { get; }
        IOrdersDetailRepository OrdersDetailRepository { get; }
        IOrdersPaymentRepository OrdersPaymentRepository { get; }
        IPaymentMethodRepository PaymentMethodRepository { get; }
        IClinicRepository ClinicsRepository { get; }
        IMedicalRecordsRepository MedicalRecordsRepository { get; }
        IMedicalRecordsDiagnosesRepository MedicalRecordsDiagnosesRepository { get; }
        IMedicalRecordsNotesRepository MedicalRecordsNotesRepository { get; }
        IMedicalRecordsPrescriptionsRepository MedicalRecordsPrescriptionsRepository { get; }
        IPrescriptionFrequentsRepository PrescriptionFrequentsRepository { get; }
        INotificationsRepository NotificationsRepository { get; }
        IProductStockHistoricalRepository ProductStockHistoricalRepository { get; }
    }
}
