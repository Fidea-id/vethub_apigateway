﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Requests.Masters
{
    public class UserSubsRequest
    {
        public int UserId { get; set; }
        public int SubscriptionId { get; set; }
    }
}
