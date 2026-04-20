using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain.IRepositories
{
    public interface IOfferRepository
    {
        public Task<bool> CreateOffer(Offers offer);
        public Task<List<Offers>> GetOffers(string workshopId);
        public Task<Object> GetOfferById(string workshopId);
    }
}
