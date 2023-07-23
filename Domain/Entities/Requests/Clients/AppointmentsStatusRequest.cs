namespace Domain.Entities.Requests.Clients
{
    public class AppointmentsStatusRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Weight { get; set; }
    }
}
