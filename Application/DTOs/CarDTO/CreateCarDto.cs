using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs.CarDTO
{
    public class CreateCarDto
    {
        public string CarModel { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public string PlateNumber { get; set; }
        public int CurrentKm { get; set; }
        public string OilType { get; set; }
        public string customerId { get; set; }

    }
}
