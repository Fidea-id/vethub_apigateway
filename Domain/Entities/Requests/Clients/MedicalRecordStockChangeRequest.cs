namespace Domain.Entities.Requests.Clients
{
    public class MedicalRecordStockChangeRequest
    {
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public bool RestoreStock { get; set; }
        public int ProfileId { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
