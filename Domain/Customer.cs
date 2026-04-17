using QudraSaaS.Dmain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System;


namespace QudraSaaS.Dmain
{
    public class Customer: applicationUser
    {
        
        public int numberOfVisits { get; set; }
        public Ranks rank { get; set; }= Ranks.عادي;
        //public DateTime lastSession {  get; set; }
        public DateTime createdAt { get; set; }
        public string notes { get; set; }
        //------------------------------

        
        public string workShopId { get; set; }

        //[ForeignKey("workShopId")]
        //public Workshop workShop { get; set; }

        //--------------------
        //public ICollection<Car> cars { get; set; }
        //public ICollection<ServiceSession> serviceSessions { get; set; }

    }
}
