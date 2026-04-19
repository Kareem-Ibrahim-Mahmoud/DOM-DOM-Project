using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs.User
{
    public class GetUserDto
    {
        public string Id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string whats { get; set; }
        public string notes { get; set; }
        public string? email { get; set; }
        public int numOfSession { get; set; }
        public Ranks rank { get; set; }
    }
}
