using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain
{
    public class Offers
    {
        public int id { get; set; }
        public string message { get; set; }
        public string WorkshopId { get; set; }
        public Ranks rank { get; set; }
    }
}
