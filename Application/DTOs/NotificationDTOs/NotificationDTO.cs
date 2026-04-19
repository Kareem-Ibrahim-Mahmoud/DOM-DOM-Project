using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs.NotificationDTOs
{
    public class NotificationDTO
    {
        public string Message { get; set; }
        public NotificationTypeEnum Type { get; set; }
        public Ranks Rank { get; set; }
    }
}

    


   


