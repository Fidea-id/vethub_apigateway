﻿namespace Domain.Entities.Requests.Clients
{
    public class ProfileRequest
    {
        public string? Name { get; set; }
        public string? ClinicName { get; set; }
        public string? Email { get; set; }
        public string? Photo { get; set; }
        public string? Roles { get; set; }
    }
}
