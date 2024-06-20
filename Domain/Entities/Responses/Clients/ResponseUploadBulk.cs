namespace Domain.Entities.Responses.Clients
{
    public class ResponseUploadBulk
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<string> validationMessage { get; set; }
    }
}
