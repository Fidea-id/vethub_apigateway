namespace Domain.Entities.Requests.Clients
{
    public class PrescriptionFrequentsFilter : BaseEntityFilter
    {
        public string? Name { get; set; }
        public string? Lang { get; set; }
    }
}
