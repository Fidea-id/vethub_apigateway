namespace Domain.Entities.Models.Clients
{
    public class MedicalRecordsNotes : BaseEntity
    {
        public int MedicalRecordsId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
