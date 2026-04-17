using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain
{
    public class OliType
    {
        public int Id { get; set; }
        public string oiltybe { get; set; }
        public double KM { get; set; }
      
        public string workshopId { get; set; }
        [ForeignKey("workshopId")]
        public Workshop workshop { get; set; }
    }
}
