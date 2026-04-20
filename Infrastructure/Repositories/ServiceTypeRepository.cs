using Microsoft.EntityFrameworkCore;
using QudraSaaS.Dmain;
using QudraSaaS.Dmain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Infrastructure.Repositories
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly Context _context;
        public ServiceTypeRepository(Context context)
        {
            _context = context;
        }

        public async Task<ServiceType> CreateAsync(ServiceType serviceType)
        {
            await _context.ServiceTypes.AddAsync(serviceType);
            await _context.SaveChangesAsync();
            return serviceType;

        }
       

        public async Task<ServiceType> UpdateAsync(ServiceType serviceType)
        {
            // Update() does NOT return a Task → you cannot await it.
            // Update() is synchronous, so it returns an EntityEntry, not a Task.
                _context.ServiceTypes.Update(serviceType);
                await _context.SaveChangesAsync();
                return serviceType;
           
        }

        public async Task<ServiceType> GetById(int id)
        {
            var serviceType = await _context.ServiceTypes.FirstOrDefaultAsync(st=>st.Id==id);

            return (serviceType);

            // what about nulls ? handle this later
        }

        public async Task<ServiceType> Delete(ServiceType serviceType)
        {
            //_context.Remove(serviceType);
            _context.ServiceTypes.Remove(serviceType);
            await _context.SaveChangesAsync();
            return serviceType; // do not return the entity 
        }

        public async Task<List<ServiceType>> GetAllByWorkshopId(string workshopId)
        {
            var serviceTypes = await _context.ServiceTypes.
                 Where(st => st.workShopId == workshopId)
                 .ToListAsync();

            return serviceTypes;
        }
    }
}
