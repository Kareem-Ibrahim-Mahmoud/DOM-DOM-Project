using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs.TransferRequest
{
    public class GetTransferRequestDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime date { get; set; }
        public string resion {  get; set; }
        public TransferRequestState state { get; set; }
        public string UserId { get; set; }

    }
}
