﻿namespace Domain.Entities.Models.Clients
{
    public class Owners : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}