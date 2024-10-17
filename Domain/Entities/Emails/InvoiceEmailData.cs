
namespace Domain.Entities.Emails
{
    public class InvoiceEmailData
    {
        public string OwnerName { get; set; }
        public string ClinicName { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
    }
}
