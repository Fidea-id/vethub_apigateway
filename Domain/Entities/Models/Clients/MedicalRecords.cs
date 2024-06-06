namespace Domain.Entities.Models.Clients
{
    public class MedicalRecords : BaseEntity
    {
        public string Code { get; set; }
        public int AppointmentId { get; set; }
        public int StaffId { get; set; }
        public int PatientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string PaymentStatus { get; set; }
        public string? DiscountMethod { get; set; }
        public double? DiscountValue { get; set; }
        public double? DiscountTotal { get; set; }
        public double? TotalDiscounted { get; set; }
        public double Total { get; set; }
    }
}
