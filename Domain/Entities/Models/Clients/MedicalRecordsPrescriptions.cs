namespace Domain.Entities.Models.Clients
{
    public class MedicalRecordsPrescriptions : BaseEntity
    {
        public int MedicalRecordsId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PrescriptionFrequency { get; set; }
        public double PrescriptionAmount { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double Total { get; set; }
    }
}
