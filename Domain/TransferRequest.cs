using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain
{
    public class TransferRequest
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string code { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime date { get; set; }
        public string reason { get; set; }
        public TransferRequestState state { get; set; }
        public string UserId { get; set; }
        public string WorkShopId { get; set; }

    }
}
