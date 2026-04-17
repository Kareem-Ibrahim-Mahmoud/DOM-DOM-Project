using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain
{
    public class Workshop:applicationUser
    {
        
        public string address { get; set; }
        public string imageUrl { get; set; }
        public string code { get; set; }
        public string workingHours {  get; set; }
        public WorkshopState state { get; set; }= WorkshopState.Active;
        public DateTime SubstitutionDate { get; set; }
        public DateTime EndDate { get; set; }
        //------------------------------
        //public ICollection<Rank> ranks { get; set; }
        public ICollection<ServiceSession> serviceSessions { get; set; }
        public ICollection<ServiceType> serviceTypes { get; set; }

    }

}
