﻿namespace Domain.Entities.Filters.Clients
{
    public class ClinicsFilter : BaseEntityFilter
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}