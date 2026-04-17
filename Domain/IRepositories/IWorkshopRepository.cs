using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain.IRepositories
{
    public interface IWorkshopRepository
    {
        public Task<Workshop> GetWorkshopById(string id);
        public Task<Workshop> GetWorkshopByCode(string code);
        public Task<workshopHomepage> WorkshopHomepage(string workshopId);
        public Task<object> WorkshopHomePage2(string workshopId);
        public Task<List<RanksAdminDto>> RanksAdminDto(string workshopId);
        public Task<List<Offers>> listOffers(string workshopId);
        public Task<int> GetAllWorkshopsInState(WorkshopState state);
        public Task<int> GetWorkshopsCounts();
        public Task<List<Workshop>> GetAllWorkshops();
        public Task<bool> UpdateWorkshop(Workshop workshop);
    }
}
