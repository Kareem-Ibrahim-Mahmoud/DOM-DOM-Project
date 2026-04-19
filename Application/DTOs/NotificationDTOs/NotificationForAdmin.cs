using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs.NotificationDTOs
{
    public class NotificationForAdmin
    {
        public string message {  get; set; }
        public NotificationTypeEnum type {  get; set; }
        public DateTime date { get; set; }
        public string customerId { get; set; }
    }
}
