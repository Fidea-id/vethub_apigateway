namespace Domain.Entities.Responses.Clients
{
    public class OpnameDetailResponse
    {
        public int OpnamePatientsId { get; set; }
        public int OpnameId { get; set; }
        public string OpnameName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int EstimatedDays { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
    }
}
