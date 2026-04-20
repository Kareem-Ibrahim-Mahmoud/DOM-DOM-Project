using Microsoft.AspNetCore.Identity;
using QudraSaaS.Application.DTOs.ServiceTypeDTOs;
using QudraSaaS.Application.Interface;
using QudraSaaS.Application.IServices;
using QudraSaaS.Dmain;
using QudraSaaS.Dmain.IRepositories;
using QudraSaaS.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly UserManager<applicationUser> userManager;

        // this service needs 2 repos (ServiceTypeRepo , WorkshopRepo)
        private readonly IServiceTypeRepository _repo; 
        private readonly IWorkshopRepository _workshopRepo;
        public ServiceTypeService(UserManager<applicationUser> userManager, IServiceTypeRepository repo ,IWorkshopRepository workshopRepo)
        {
            this.userManager = userManager;
            _repo = repo;
            _workshopRepo = workshopRepo;
        }
        //                ServiceTypeDto
        public async Task<ServiceType> CreateAsync(ClaimsPrincipal userPrincipal, ServiceTypeCreateDTO dto)
        {
            var user = await userManager.GetUserAsync(userPrincipal);

            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }

            var serviceType = new ServiceType();
            serviceType.Name = dto.Name;
            serviceType.workShopId = user.Id;
            serviceType.available = true;

            return await _repo.CreateAsync(serviceType);

        }
        //                bool
        //      difference between Put,Patch
        public async Task<ServiceType> Update(ServiceTypeDTO dto , int serviceTypeId)
        {
            var serviceType = await _repo.GetById(serviceTypeId);

            if (serviceType == null)
                return null;
            // The function stops here.
            // Anything written after return will only run when serviceType is NOT null.
            // Guard Clause (clean, professional)

            serviceType.Name=dto.Name;
            //serviceType.workShopId = dto.WorkshopId;
            serviceType.available = true;

            return await _repo.UpdateAsync(serviceType);


        }
                          //bool
        public async Task<ServiceType> Delete(int serviceTypeId)
        {
            var serviceType = await _repo.GetById(serviceTypeId);

            if(serviceType== null)
            {
                return null;
            }

           return await _repo.Delete(serviceType);
           

        }

        public async Task<List<GetServiceType>> GetAllByWorkshopId(ClaimsPrincipal userPrincipal)
        {
            var user = await userManager.GetUserAsync(userPrincipal);

            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }

            // this GetbyId works for customer , i need another one but for workshops 
            var existing = await _workshopRepo.GetWorkshopById(user.Id);

            if(existing==null)
            {
                return null;
            }

            var serviceTypes = await _repo.GetAllByWorkshopId(user.Id);

            // Map to DTO
            return serviceTypes.Select(st => new GetServiceType
            {
                Name = st.Name,
                id = st.Id,
            }).ToList();

             
        }
    }

}
