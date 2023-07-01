namespace Domain.Entities.Models.Clients
{
    public class Appointments : BaseEntity
    {
        public int OwnersId { get; set; }
        public int PatientsId { get; set; }
        public DateTime Date { get; set; }
        public int StaffId { get; set; }
        public int ServiceId { get; set; }
        public string Notes { get; set; }
    }
}
