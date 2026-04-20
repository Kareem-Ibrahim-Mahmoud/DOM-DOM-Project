using QudraSaaS.Application.DTOs.TransferRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interface
{
    public interface ITransferRequestService
    {
        public Task<bool> TransferRequestState(int RequestId, bool state);
        public Task<List<GetTransferRequestDto>> GetAllRequest(ClaimsPrincipal userPrincipal);
        public Task<bool> CreateTransferRequest(ClaimsPrincipal userPrincipal, CreateTransferRequestDto requestDto);
        
    }
}
