using QudraSaaS.Application.DTOs.OfferDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interface
{
    public interface IOffersService
    {
        public Task<bool> CreateOffer(ClaimsPrincipal userPrincipal, CreateOfferDto offerDto);
        public Task<List<GetOferrsDto>> GetOffers(ClaimsPrincipal userPrincipal);
        public Task<OffersProfileDto> OffersProfile(ClaimsPrincipal userPrincipal);
    }
}
