using Microsoft.AspNetCore.Identity;
using QudraSaaS.Application.DTOs.OfferDto;
using QudraSaaS.Application.Interface;
using QudraSaaS.Dmain;
using QudraSaaS.Dmain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Repo
{
    public class OffersService: IOffersService
    {
        private readonly UserManager<applicationUser> userManager;
        private readonly IOfferRepository offerRepository;
        private readonly IUserRepository userRepository;
        private readonly INotificationRepository notificationRepository;

        public OffersService(UserManager<applicationUser> userManager, IOfferRepository offerRepository, IUserRepository userRepository, INotificationRepository notificationRepository)
        {
            this.userManager = userManager;
            this.offerRepository = offerRepository;
            this.userRepository = userRepository;
            this.notificationRepository = notificationRepository;
        }
        public async Task<bool> CreateOffer(ClaimsPrincipal userPrincipal,CreateOfferDto offerDto)
        {
            var user = await userManager.GetUserAsync(userPrincipal);

            if (user == null)
            {
                throw new InvalidOperationException("Workshop user not found.");
            }
            //create offer
            Offers offer = new Offers
            {
                message = offerDto.message,
                rank = offerDto.rank,
                WorkshopId = user.Id
            };
            await offerRepository.CreateOffer(offer);
            //send notification to customers where rank equal offer rank

            var customersIds = await userRepository.CustomersIdsByRankId(offerDto.rank,user.Id);
            
            await notificationRepository.SendNotificationsToList(customersIds, new Notification
            {
                Message = offerDto.message,
                Type = Dmain.Enums.NotificationTypeEnum.Offers,
                SendingDate = DateTime.Now,
                WorkshopId = user.Id
            });

            return true;
        }
    
        public async Task<List<GetOferrsDto>>GetOffers(ClaimsPrincipal userPrincipal)
        {
            var user = await userManager.GetUserAsync(userPrincipal);

            if (user == null)
            {
                throw new InvalidOperationException("Workshop user not found.");
            }
            var offers= await offerRepository.GetOffers(user.Id);
            var offersDto=new List<GetOferrsDto>();
            foreach (var offer in offers)
            {
                offersDto.Add(new GetOferrsDto
                {
                    message=offer.message, 
                    rank = offer.rank,
                });
            }
            return offersDto;
        }
    
        public async Task<OffersProfileDto> OffersProfile(ClaimsPrincipal userPrincipal)
        {
            var user = await userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("Workshop user not found.");
            }
           var resalte=await offerRepository.GetOfferById(user.Id);
            return new OffersProfileDto
            {
                rank0 = ((dynamic)resalte).rank0,
                rank1 = ((dynamic)resalte).rank1,
                rank2 = ((dynamic)resalte).rank2,
                rank3 = ((dynamic)resalte).rank3,
                rank4 = ((dynamic)resalte).rank4,
            };
        }
    }
}
