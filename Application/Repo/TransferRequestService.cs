using Microsoft.AspNetCore.Identity;
using QudraSaaS.Application.DTOs.TransferRequest;
using QudraSaaS.Application.Interface;
using QudraSaaS.Dmain;
using QudraSaaS.Dmain.IRepositories;
using QudraSaaS.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Repo
{
    public class TransferRequestService: ITransferRequestService
    {
        private readonly UserManager<applicationUser> userManager;
        private readonly ITransferRequest transferRequest;
        private readonly IWorkshopRepository workshopRepository;
        private readonly IUserRepository userRepository;

        public TransferRequestService(UserManager<applicationUser> userManager, ITransferRequest transferRequest,IWorkshopRepository workshopRepository, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.transferRequest = transferRequest;
            this.workshopRepository = workshopRepository;
            this.userRepository = userRepository;
        }

        public async Task<bool> CreateTransferRequest(ClaimsPrincipal userPrincipal, CreateTransferRequestDto requestDto)
        {
            var user = await userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }
            var workshop = await workshopRepository.GetWorkshopByCode(requestDto.code);
            if (workshop == null)
            {
                return false;
            }
            Customer userr =await userRepository.getUserById(user.Id);
            userr.workShopId=workshop.Id;
            await userRepository.UpdateUser(userr);

            //var tradeRequest = new TransferRequest
            //{
            //    UserName=user.name,
            //    UserId=user.Id,
                
            //    PhoneNumber= userr.phone,
            //    code=requestDto.code,
            //    date=DateTime.Now,
            //    reason=requestDto.reason,
            //    state=Dmain.Enums.TransferRequestState.UnderReview,
            //    WorkShopId= userr.workShopId
            //};
            //if(await transferRequest.Create(tradeRequest))
            //{
            //    return true;
            //}
            return true;

        }
    
        public async Task<List<GetTransferRequestDto>> GetAllRequest(ClaimsPrincipal userPrincipal)
        {
            var workshop = await userManager.GetUserAsync(userPrincipal);
            if (workshop == null)
            {
                throw new InvalidOperationException("invaled token");
            }
            var request=await transferRequest.GetAllRequest(workshop.Id);
            var requestsDto = new List<GetTransferRequestDto>();
            foreach (var item in request)
            {
                var requestDto = new GetTransferRequestDto
                {
                    Id=item.id,
                    resion=item.reason,
                    UserId=item.UserId,
                    UserName=item.UserName, 
                    PhoneNumber=item.PhoneNumber,
                    date=item.date,
                    state=item.state,
                };
                requestsDto.Add(requestDto);
            }
            return requestsDto;
        }
    
        public async Task<bool> TransferRequestState(int RequestId,bool state)
        {
            var request=await transferRequest.GetById(RequestId);
            if (state == false)
            {
                request.state=Dmain.Enums.TransferRequestState.Rejected;
                if(await transferRequest.Update(request))
                {
                    return true;
                }
            }
            request.state=Dmain.Enums.TransferRequestState.Accepted;
            Workshop workshop=await workshopRepository.GetWorkshopByCode(request.code);
            if (await transferRequest.Update(request))
            {
                Customer user=await userRepository.getUserById(request.UserId);
                user.workShopId= workshop.Id;
                if(await userRepository.UpdateUser(user))
                {
                    return true;
                }
            }
            return false;
        }



    }

}
