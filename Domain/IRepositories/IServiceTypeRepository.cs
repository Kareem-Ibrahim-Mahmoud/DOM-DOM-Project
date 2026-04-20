using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain.IRepositories
{
    public interface IServiceTypeRepository
    {
        Task<ServiceType> CreateAsync(ServiceType serviceType);

        Task<ServiceType> UpdateAsync(ServiceType serviceType);

        Task<ServiceType> GetById(int id);

        
        Task<ServiceType> Delete(ServiceType serviceType);

        Task<List<ServiceType>> GetAllByWorkshopId(string workshopId);

    }
}
