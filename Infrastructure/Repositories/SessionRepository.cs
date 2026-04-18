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
    public class SessionRepository: ISessionRepository
    {
        private readonly Context context;

        public SessionRepository(Context context)
        {
            this.context = context;
        }

        public async Task<bool>CreateSession(ServiceSession serciceSession)
        {
            await context.ServiceSessions.AddAsync(serciceSession);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<ServiceSession>GetSessionById(int Id)
        {
            var Session = await context.ServiceSessions.FirstOrDefaultAsync(s => s.id == Id);
            if (Session == null)
            {
                throw new InvalidOperationException("Session not found");
            }
            return Session;
        }

        public async Task<List<ServiceSession>>GetSessionsforCar(int carId)
        {
            return await context.ServiceSessions.Where(x=>x.carId == carId).ToListAsync();
            
        }

        public async Task<List<ServiceSession>> GetSessionsforUser(string userId)
        {
            return await context.ServiceSessions.Where(x => x.customerId == userId).ToListAsync();

        }

        public async Task<List<ServiceSession>> GetSessionsforWorkshop(string WorkshopId)
        {
            return await context.ServiceSessions.Where(x => x.workShopId == WorkshopId).ToListAsync();

        }


        public async Task<bool>UpdateAsync(ServiceSession session)
        {
            context.ServiceSessions.Update(session);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteServiceSession(int id)
        {
            var ser = await context.ServiceSessions.FirstOrDefaultAsync(x => x.id == id);
            if (ser == null)
            {
                throw new InvalidOperationException($"Could not find ServiceSession with id : {id}");
            }
            context.ServiceSessions.Remove(ser);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteServiceSessionWithUserId(string UserId)
        {
            var cars = await context.ServiceSessions.Where(c => c.customerId == UserId).ToListAsync();
            context.RemoveRange(cars);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
