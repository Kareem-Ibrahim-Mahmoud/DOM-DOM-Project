using QudraSaaS.Application.DTOs.SessionDto;
using QudraSaaS.Dmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs.User
{
    public class UserHomePage
    {
        public int nextChange { get; set; }
        public List<GetSessionDto> lastSession { get; set; }=new List<GetSessionDto>();
        public string Rank { get; set; }
        public int numberOfSessions { get; set; }
    }
}
