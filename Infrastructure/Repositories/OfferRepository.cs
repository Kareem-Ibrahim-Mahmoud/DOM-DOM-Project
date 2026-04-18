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
    public class OfferRepository: IOfferRepository
    {
        private readonly Context context;

        public OfferRepository(Context context)
        {
            this.context = context;
        }
        public async Task<bool> CreateOffer(Offers offer)
        {
            await context.offers.AddAsync(offer);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Offers>>GetOffers(string workshopId)
        {
            var offers = await context.offers.Where(x => x.WorkshopId == workshopId).ToListAsync();
            return offers;
        }

        public async Task<Object> GetOfferById(string workshopId)
        {
            var user0 = context.Customers.Where(x => x.workShopId == workshopId && x.rank == Ranks.عادي).Count();
            var user1 = context.Customers.Where(x => x.workShopId == workshopId && x.rank == Ranks.برونزي).Count();
            var user2 = context.Customers.Where(x => x.workShopId == workshopId && x.rank == Ranks.فضي).Count();
            var user3 = context.Customers.Where(x => x.workShopId == workshopId && x.rank == Ranks.ذهبي).Count();
            var user4 = context.Customers.Where(x => x.workShopId == workshopId && x.rank == Ranks.البلاتيني).Count();
            return new
            {
                rank0 = user0,
                rank1 = user1,
                rank2 = user2,
                rank3 = user3,
                rank4 = user4
            };

        }
    }
}
