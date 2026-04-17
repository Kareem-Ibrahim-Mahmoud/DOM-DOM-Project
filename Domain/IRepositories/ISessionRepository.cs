using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain.IRepositories
{
    public interface ISessionRepository
    {
        public Task<bool> CreateSession(ServiceSession serciceSession);
        public Task<ServiceSession> GetSessionById(int Id);
        public Task<List<ServiceSession>> GetSessionsforCar(int carId);
        public Task<bool> UpdateAsync(ServiceSession session);
        public Task<List<ServiceSession>> GetSessionsforWorkshop(string WorkshopId);
        public Task<List<ServiceSession>> GetSessionsforUser(string userId);
        public  Task<bool> deleteServiceSession(int id);
        public Task<bool> DeleteServiceSessionWithUserId(string UserId);
    }
}
