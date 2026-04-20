using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs.AdminDtos
{
    public class WorkshopsManagementDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string phone { get; set; }
        public WorkshopState State { get; set; }
        public DateTime SubstitutionDate { get; set; }
        public DateTime EndDate { get; set; }
        public int totalCustomers { get; set; }
        public string whats { get; set; }

    }
}
