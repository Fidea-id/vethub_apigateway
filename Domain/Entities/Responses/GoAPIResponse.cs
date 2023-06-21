namespace Domain.Entities.Responses
{
    public class GoAPIData
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class GoAPIResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<GoAPIData> data { get; set; }
    }
}
