using QudraSaaS.Dmain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs.SessionDto
{
    public class CreateSessionDto
    {
        public int?KmReading { get; set; }// at the time of service
        public int? NumberOfKilometers { get; set; }// How many kilometers does the oil get you?
        public bool FilterChanged { get; set; }
        public bool OilChanged { get; set; }
        public int? OilId { get; set; }
        public List<string>? AdditionalServices { get; set; } 
        public int? NextChange { get; set; }//after how many kilometers the next service is due
        public string? description { get; set; }
        public DateTime sessionDate { get; set; }
        public double cost { get; set; }
        //------------------------------

        public int carId { get; set; }
        public string customerId { get; set; }
    }
}
