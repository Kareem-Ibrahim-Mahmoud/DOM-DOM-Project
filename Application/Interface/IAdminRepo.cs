using QudraSaaS.Application.DTOs.AdminDtos;
using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interface
{
    public interface IAdminRepo
    {
        public Task<OverviewCardsDto> GetOverviewCardsData();
        public Task<List<WorkshopsManagementDto>> GetWorkshopsManagement();
        public Task<bool> AddTime(string workshopId, int newEndDate);
        public Task<bool> ChangeState(string workshopId, WorkshopState newState);

    }
}
