using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain.IRepositories
{
    public interface IFCMRepository
    {
        Task<bool> AddToken(FCM fcm);
        Task<List<FCM>> GetTokensByUserId(string userId);
        Task<bool> RemoveTokenByDeviceId(string deviceId);
    }
}
