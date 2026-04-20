using QudraSaaS.Dmain;
using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs
{
    public class RegesterCustmer
    {
        //--------------------add User
        [Required]
        public string name { get; set; }
        public string? Email { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string whats { get; set; }
        public string notes { get; set; }
        //----------------------- add Car
        public string CarModel { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public string PlateNumber { get; set; }
        public int CurrentKm { get; set; }
        public string OilType { get; set; }

    }
}
