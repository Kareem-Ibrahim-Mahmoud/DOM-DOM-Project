using QudraSaaS.Application.DTOs.ServiceTypeDTOs;
using QudraSaaS.Dmain;
//using QudraSaaS.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.IServices
{
    public interface IServiceTypeService
    {
        Task<ServiceType> CreateAsync(ClaimsPrincipal userPrincipal, ServiceTypeCreateDTO dto);
        Task<ServiceType> Update(ServiceTypeDTO dto , int serviceTypeId);

        Task<ServiceType> Delete(int serviceTypeId);

        Task<List<GetServiceType>> GetAllByWorkshopId(ClaimsPrincipal userPrincipal);
    }
}
