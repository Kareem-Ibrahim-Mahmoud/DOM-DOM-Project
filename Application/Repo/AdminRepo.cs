using QudraSaaS.Application.DTOs.AdminDtos;
using QudraSaaS.Application.Interface;
using QudraSaaS.Dmain.Enums;
using QudraSaaS.Dmain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Repo
{
    public class AdminRepo: IAdminRepo
    {
        private readonly IWorkshopRepository workshopRepository;

        public AdminRepo(IUserRepository userRepository, IWorkshopRepository workshopRepository)
        {
            UserRepository = userRepository;
            this.workshopRepository = workshopRepository;
        }

        public IUserRepository UserRepository { get; }

        public async Task<OverviewCardsDto> GetOverviewCardsData()
        {

            return new OverviewCardsDto
            {
                TotalUsers =await UserRepository.GetAllCustomers(),
                TotalWorkshops =await workshopRepository.GetWorkshopsCounts(),
                TotalActiveWorkshops = await workshopRepository.GetAllWorkshopsInState(Dmain.Enums.WorkshopState.Active),
                TotalInactiveWorkshops = await workshopRepository.GetAllWorkshopsInState(Dmain.Enums.WorkshopState.Inactive),
                TotalExperimentalWorkshops = await workshopRepository.GetAllWorkshopsInState(Dmain.Enums.WorkshopState.Experimental)
            };
        }

        public async Task<List<WorkshopsManagementDto>> GetWorkshopsManagement()
        {
            var workshops = await workshopRepository.GetAllWorkshops();
            var workshopsManagement = new List<WorkshopsManagementDto>();
            foreach (var workshop in workshops)
            {
                workshopsManagement.Add(new WorkshopsManagementDto
                {
                    Id = workshop.Id,
                    Name = workshop.name,
                    Code = workshop.code,
                    phone = workshop.phone,
                    State = workshop.state,
                    SubstitutionDate = workshop.SubstitutionDate,
                    EndDate = workshop.EndDate,
                    whats = workshop.whats,
                    totalCustomers = await UserRepository.GetCustomersCountByWorkshopId(workshop.Id)
                
                });

            }
            return workshopsManagement;
        }
    
        public async Task<bool>AddTime(string workshopId, int newEndDate)
        {
            var workshop = await workshopRepository.GetWorkshopById(workshopId);
            if (workshop == null)
            {
                throw new InvalidOperationException("workshop not found");
            }

            workshop.EndDate =DateTime.Now.AddMonths(newEndDate);
            workshop.SubstitutionDate = DateTime.Now;
            return await workshopRepository.UpdateWorkshop(workshop);
        }

        public async Task<bool> ChangeState(string workshopId,WorkshopState newState)
        {
            var workshop = await workshopRepository.GetWorkshopById(workshopId);
            if (workshop == null)
            {
                throw new InvalidOperationException("workshop not found");
            }
            workshop.state = newState;
            return await workshopRepository.UpdateWorkshop(workshop);
        }


    }
}
