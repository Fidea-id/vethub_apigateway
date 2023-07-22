namespace Domain.Entities.Models.Clients
{
    public class AppointmentsDetail : BaseEntity
    {
        public int AppointmentId { get; set; }
        public int MedicalRecordId { get; set; }
        public int InvoiceId { get; set; }
        public int TotalPrice { get; set; }
        public DateTime EndDate { get; set; }
    }
}
