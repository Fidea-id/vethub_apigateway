using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Emails
{
    public class ForgotPasswordData
    {
        public string Name { get; set; }
        public string URLActivation { get; set; }
        public DateTime Date { get; set; }
    }
}
