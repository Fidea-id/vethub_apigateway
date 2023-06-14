﻿namespace Domain.Entities
{
    public class BaseURI
    {
        public string MasterAPIURI { get; set; }
        public string ClientAPIURI { get; set; }
        public string APIURI { get; set; }
    }
    public enum APIType
    {
        Client, Master
    }
}
