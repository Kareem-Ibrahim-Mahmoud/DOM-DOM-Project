using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain
{
    public class Notification
    {
        public int Id {  get; set; }
       
        public string Message { get; set; }

        public NotificationTypeEnum Type { get; set; }

        //type is enum 
        //rank is enum ?? getallranks by workshopid
        public DateTime SendingDate { get; set; }


        public string WorkshopId { get; set; }  // FK to Workshop table
        [ForeignKey("WorkshopId")]
        //public Workshop Workshop { get; set; }
        public string CustomerId { get; set; }  // FK to Customer table
        //[ForeignKey("CustomerId")]
        //public Customer Customer { get; set; }





        //public Customer Customer { get; set; } ==> this for alerts 
        // for offers and tips for group of customers by rank
        //public List<Customer> Customers { get; set; }



    }
}
