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
    public class FCMRepository: IFCMRepository
    {
        private readonly Context context;

        public FCMRepository(Context context)
        {
            this.context = context;
        }
        public async Task<bool> AddToken(FCM fcm)
        {
            var existingToken = context.fcms.FirstOrDefault(t => t.DeviceId == fcm.DeviceId);
            if (existingToken != null)
            {
                existingToken.Token = fcm.Token;
                existingToken.UserId = fcm.UserId;
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                context.fcms.Add(fcm);
                await context.SaveChangesAsync();
                return true;
            }  
        }

        public async Task<List<FCM>> GetTokensByUserId(string userId)
        {
            return await context.fcms.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<bool> RemoveTokenByDeviceId(string deviceId)
        {
            var token = context.fcms.FirstOrDefault(t => t.DeviceId == deviceId);
            if (token == null)
            {
                throw new InvalidOperationException("Token not found");
            }
            context.fcms.Remove(token);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
