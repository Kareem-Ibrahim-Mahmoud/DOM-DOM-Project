using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs.RankDTOs
{
    public class RankCreateDTO
    {
        public string Name { get; set; }
        public int MinVisits { get; set; }
        public int DiscountPercentage { get; set; }// 5 means 5%
    }
}
