namespace Domain.Entities.Responses.Clients
{
    public class AppointmentsDetailResponse
    {
        public int OwnersId { get; set; }
        public string OwnersName { get; set; }
        public string OwnersTitle { get; set; }
        public int PatientsId { get; set; }
        public string PatientsName { get; set; }
        public string PatientsBreed { get; set; }
        public DateTime Date { get; set; }
        public int? DurationEstimate { get; set; }
        public string? DurationTypeEstimate { get; set; }
        public DateTime? EndDateEstimate { get; set; }
        public int? DurationActual { get; set; }
        public string? DurationTypeActual { get; set; }
        public DateTime? EndDateActual { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string Notes { get; set; }
    }
}
