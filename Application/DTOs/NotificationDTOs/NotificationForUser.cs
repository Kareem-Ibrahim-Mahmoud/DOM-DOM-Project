using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs.NotificationDTOs
{
    public class NotificationForUser
    {
        public string Message { get; set; }
        public string customerId { get; set; }
        public NotificationTypeEnum Type { get; set; }
    }
}
