namespace Domain.Entities.Emails
{
    public class ConfirmationEmailData
    {
        public string Name { get; set; }
        public string ClinicName { get; set; }
        public string URLActivation { get; set; }
        public DateTime Date { get; set; }
    }
}
