using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.Masters
{
    public class UsersToken : BaseEntity
    {
        public int UserId { get; set; }
        public string FCMToken { get; set; }
    }
}
