﻿namespace Domain.Entities.Models.Clients
{
    public class Clinics : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string WebUrl { get; set; }
        public string MapUrl { get; set; }
    }
}