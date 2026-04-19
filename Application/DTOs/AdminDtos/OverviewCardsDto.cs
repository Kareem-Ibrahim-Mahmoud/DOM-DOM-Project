using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs.AdminDtos
{
    public class OverviewCardsDto
    {
        public int TotalUsers { get; set; }
        public int TotalWorkshops { get; set; }
        public int TotalActiveWorkshops { get; set; }
        public int TotalInactiveWorkshops { get; set; }
        public int TotalExperimentalWorkshops { get; set; }

    }
}
