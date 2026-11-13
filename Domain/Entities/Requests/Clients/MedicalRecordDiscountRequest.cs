namespace Domain.Entities.Requests.Clients
{
    public class MedicalRecordDiscountRequest
    {
        public string? DiscountMethod { get; set; }
        public double? DiscountValue { get; set; }
    }
}
