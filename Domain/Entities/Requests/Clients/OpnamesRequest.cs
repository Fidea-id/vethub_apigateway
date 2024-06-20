namespace Domain.Entities.Requests.Clients
{
    public class OpnamesRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double? BasePrice { get; set; }
    }
}
