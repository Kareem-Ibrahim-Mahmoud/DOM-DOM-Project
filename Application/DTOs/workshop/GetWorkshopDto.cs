using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs.workshop
{
    public class GetWorkshopDto
    {
        public string name {  get; set; }
        public string address {  get; set; }
        public string PhoneNumber { get; set; }
        public string whats { get; set; }
        public string workingHours {  get; set; }
        public List<string> serviceTypes {  get; set; }=new List<string>();
    }
}
