using Microsoft.EntityFrameworkCore;
using QudraSaaS.Dmain;
using QudraSaaS.Dmain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Infrastructure.Repositories
{
    public class TransferRequestRepository: ITransferRequest
    {
        private readonly Context context;

        public TransferRequestRepository(Context context)
        {
            this.context = context;
        }

        public async Task<bool>Create(TransferRequest transfer)
        {
            await context.transferRequests.AddAsync(transfer);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TransferRequest>>GetAllRequest(string workshopId)
        {
            return await context.transferRequests
                .Where(x => x.WorkShopId == workshopId).ToListAsync();
        }

        public async Task<TransferRequest> GetById(int requestId)
        {
            var request= await context.transferRequests.FirstOrDefaultAsync(x => x.id == requestId);
            if(request == null)
            {
                throw new InvalidOperationException("The request not found");
            }
            return request;
        }

        public async Task<bool>Update( TransferRequest transfer)
        {
            context.transferRequests.Update(transfer);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
