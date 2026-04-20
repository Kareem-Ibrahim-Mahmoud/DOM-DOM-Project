using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain.IRepositories
{
    public interface ITransferRequest
    {
        public Task<bool> Create(TransferRequest transfer);
        public Task<List<TransferRequest>> GetAllRequest(string workshopId);
        public Task<TransferRequest> GetById(int requestId);
        public Task<bool> Update(TransferRequest transfer);
    }
}
