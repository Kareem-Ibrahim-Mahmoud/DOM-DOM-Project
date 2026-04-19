using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs.OliTypeDTO
{
    public class GetAllByworkshop
    {
        public int Id { get; set; }
        public string oiltybe { get; set; }
        public double KM { get; set; }

        public string workshopId { get; set; }
    }
}
