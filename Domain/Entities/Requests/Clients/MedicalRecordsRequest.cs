﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Clients
{
    public class MedicalRecordsRequest
    {
        public string? Code { get; set; }
        public int AppointmentId { get; set; }
        public int StaffId { get; set; }
        public int PatientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PaymentStatus { get; set; }
        public double? Total { get; set; }
    }
}