using Microsoft.EntityFrameworkCore;
using QudraSaaS.Dmain;
using QudraSaaS.Dmain.Enums;
using QudraSaaS.Dmain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Infrastructure.Repositories
{
    public class WorkshopRepository : IWorkshopRepository
    {
        private readonly Context context;

        public WorkshopRepository(Context context)
        {
            this.context = context;
        }

        public async Task<Workshop> GetWorkshopById(string id)
        {
            var workshop = await context.Workshops.FirstOrDefaultAsync(x => x.Id == id);
            if (workshop == null)
            {
                throw new InvalidOperationException("workshop not found");

            }
            return workshop;
        }
        public async Task<Workshop> GetWorkshopByCode(string code)
        {
            var workshop = await context.Workshops.FirstOrDefaultAsync(x => x.code == code);
            if (workshop == null)
            {
                throw new InvalidOperationException("workshop not found");

            }
            return workshop;
        }

        public async Task<workshopHomepage> WorkshopHomepage(string workshopId)
        {
            int numberOfUsers = await context.Customers.Where(c => c.workShopId == workshopId).CountAsync();
            var sessions = await context.ServiceSessions.Where(s => s.workShopId == workshopId).CountAsync();
            var workshop = await context.Workshops.FirstOrDefaultAsync(x => x.Id == workshopId);

            return new workshopHomepage
            {
                numberOfUsers = await context.Customers.Where(c => c.workShopId == workshopId).CountAsync(),
                numberOfSessions = await context.ServiceSessions.Where(s => s.workShopId == workshopId).CountAsync(),
                name = workshop.name
            };
        }

        public async Task<object> WorkshopHomePage2(string workshopId)
        {

            var topCustomers = await context.Customers
                .Where(c => c.workShopId == workshopId)
                .OrderByDescending(c => c.numberOfVisits)
                .Take(2)
                .Select(c => new
                {
                    c.Id,
                    c.name,
                    c.phone,
                    c.numberOfVisits,
                })
                .ToListAsync();



            var lastSessions = await context.ServiceSessions
                .Where(x => x.workShopId == workshopId)
                .OrderByDescending(x => x.date)
                .Take(2)
                .Select(c => new
                {
                    c.cost,
                    c.UserName,
                    c.CarModel,
                    c.date,
                    c.description
                })
                .ToListAsync();
            return new
            {
                CustomerList = topCustomers,
                sessionsList = lastSessions
            };
        }

        public async Task<List<RanksAdminDto>> RanksAdminDto(string workshopId)
        {
            var Plain = context.Customers.Where(x => x.rank == Dmain.Enums.Ranks.عادي && x.workShopId == workshopId).Count();
            var Silver = context.Customers.Where(x => x.rank == Dmain.Enums.Ranks.فضي && x.workShopId == workshopId).Count();
            var Gold = context.Customers.Where(x => x.rank == Dmain.Enums.Ranks.ذهبي && x.workShopId == workshopId).Count();
            var Bronze = context.Customers.Where(x => x.rank == Dmain.Enums.Ranks.برونزي && x.workShopId == workshopId).Count();
            var Platinum = context.Customers.Where(x => x.rank == Dmain.Enums.Ranks.البلاتيني && x.workShopId == workshopId).Count();
            var list = new List<RanksAdminDto>
            {
                new RanksAdminDto { Name = "عادي", NumberOfUsers = Plain },
                new RanksAdminDto { Name = "برونزي", NumberOfUsers = Bronze },
                new RanksAdminDto { Name = "فضي", NumberOfUsers = Silver },
                new RanksAdminDto { Name = "ذهبي", NumberOfUsers = Gold },
                new RanksAdminDto { Name = "البلاتيني", NumberOfUsers = Platinum }
            };
            return list;
        }

        public async Task<List<Offers>> listOffers(string workshopId)
        {
            var offers = await context.offers.Where(x => x.WorkshopId == workshopId).Take(4).ToListAsync();
            return offers;
        }

        public async Task<int> GetWorkshopsCounts()
        {
            return await context.Workshops.CountAsync();
        }
        public async Task<List<Workshop>> GetAllWorkshops()
        {
            return await context.Workshops.ToListAsync();
        }
        public async Task<int> GetAllWorkshopsInState(WorkshopState state)
        {
            return await context.Workshops.Where(x => x.state == state).CountAsync();
        }

        public async Task<bool> UpdateWorkshop(Workshop workshop)
        {
            var existingWorkshop = await context.Workshops.FirstOrDefaultAsync(x => x.Id == workshop.Id);
            if (existingWorkshop == null)
            {
                throw new InvalidOperationException("workshop not found");
            }
            existingWorkshop.state = workshop.state;
            existingWorkshop.SubstitutionDate = workshop.SubstitutionDate;
            existingWorkshop.EndDate = workshop.EndDate;
            context.Workshops.Update(existingWorkshop);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
