﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Masters
{
    public class UserDemoRequest
    {
        public string Name { get; set; }
        public string ClinicName { get; set; }
        public string PhoneNumber { get; set; }
    }
}