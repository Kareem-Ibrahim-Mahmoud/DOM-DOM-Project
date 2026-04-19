using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Identity;
using QudraSaaS.Application.DTOs.NotificationDTOs;
using QudraSaaS.Application.IServices;
using QudraSaaS.Dmain;
using QudraSaaS.Dmain.Enums;
using QudraSaaS.Dmain.IRepositories;
using QudraSaaS.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace QudraSaaS.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly UserManager<applicationUser> userManager;
        private readonly INotificationRepository _repo;

        private readonly IUserRepository _customerService;
        private readonly IFCMRepository fCMRepository;

        public NotificationService(UserManager<applicationUser> userManager,INotificationRepository repo , IUserRepository customerService, IFCMRepository fCMRepository)
        {
            this.userManager = userManager;
            _repo = repo;
            _customerService = customerService;
            this.fCMRepository = fCMRepository;
        }
      

        public async Task<bool> Create(ClaimsPrincipal userPrincipal,NotificationDTO dto)
        {
            // 1- Get CustomersIds ByRankId() => will return list of customers ids
            // 2- create list of notification object 
            // 3- looping on the ids of customers and create notification object in list per customer
            // 4- Create(notifications) => create the list of notification and return true or false
            var user = await userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }
   
            var CustomersIds = await _customerService.CustomersIdsByRankId(dto.Rank,user.Id);
 
            // rank id and workshop id

            var notifications = new List<Dmain.Notification>();

            foreach (var CustId in CustomersIds)
            {
                //await or not ?
                notifications.Add(new Dmain.Notification
                {
                    Message = dto.Message,
                    WorkshopId = user.Id,
                    SendingDate = DateTime.Now,
                    Type = dto.Type,
                    CustomerId = CustId
                });
                #region Send Notification to fire base

                var tokens = await fCMRepository.GetTokensByUserId(CustId);
                foreach (var token in tokens)
                {
                    var message = new FirebaseAdmin.Messaging.Message()
                    {
                        Token = token.Token,
                        Notification = new FirebaseAdmin.Messaging.Notification
                        {
                            Title = "for test...",
                            Body = dto.Message
                        }
                        //Data = new Dictionary<string, string>
                        //{
                        //    { "title", "for test..." },
                        //    { "body", dto.Message }
                        //}

                    };
                    try
                    {
                        await FirebaseMessaging.GetMessaging(FirebaseApp.GetInstance("appp1")).SendAsync(message);
                    }
                    catch (FirebaseMessagingException ex) when (ex.Message.Contains("Requested entity was not found"))
                    {
                        // لو التوكن بايظ → نحذفه من قاعدة البيانات

                        await fCMRepository.RemoveTokenByDeviceId(token.DeviceId);
                    }
                } 
                #endregion
            }
            return await _repo.Create(notifications);
        }

        public async Task<bool> CreateSingleNotification(ClaimsPrincipal userPrincipal, NotificationForUser dto)
        {
            var user = await userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }
            var customer = await _customerService.getUserById(dto.customerId);
            if (customer == null)
            {
                throw new InvalidOperationException("invaled customer id");
            }
            var notification = new Dmain.Notification
            {
                Message = dto.Message,
                WorkshopId = user.Id,
                SendingDate = DateTime.Now,
                CustomerId = dto.customerId,
                Type = dto.Type
            };

            #region Send Notification to fire base

            var tokens = await fCMRepository.GetTokensByUserId(dto.customerId);
            foreach (var token in tokens)
            {
                var message = new FirebaseAdmin.Messaging.Message()
                {
                    Token = token.Token,
                    Notification = new FirebaseAdmin.Messaging.Notification
                    {
                        Title = ".........",
                        Body = dto.Message
                    }
                };
                try
                {
                    await FirebaseMessaging.GetMessaging(FirebaseApp.GetInstance("appp1")).SendAsync(message);
                }
                catch (FirebaseMessagingException ex) when (ex.Message.Contains("Requested entity was not found"))
                {
                    // لو التوكن بايظ → نحذفه من قاعدة البيانات

                    await fCMRepository.RemoveTokenByDeviceId(token.DeviceId);
                }
            }
            #endregion

            return await _repo.CreateOne(notification);
        }
        public async Task<List<NotificationGetDTO>> GetAllNotificationsByCustomerId(ClaimsPrincipal userPrincipal)
        {
            var user = await userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }

            var notifications = await _repo.GetAllNotificationsByCustomerId(user.Id); // will retun empty or full list

           
            // mapping
            var notificationsDto = notifications.Select(n => new NotificationGetDTO
            {
                Message = n.Message,
            }).ToList();

            return notificationsDto;

           
        }

        public async Task<List<NotificationGetDTO>> GetAllNotificationsByCustomerIdAndNotificationType(ClaimsPrincipal userPrincipal, NotificationTypeEnum notificationType)
        {
            // here we can use the GetAllNotificationsByCustomerId and filter by type here without using repo at all 
            // but the besr practice doing in in another repo function and filter by customerId and type there
            var user = await userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }

            if (notificationType == NotificationTypeEnum.all)
            {
                var notifications = await _repo.GetNotificationForUser(user.Id, notificationType);

                var notificationsDto = notifications.Select(n => new NotificationGetDTO
                {
                    Message = n.Message
                }).ToList();

                return notificationsDto;
            }
            else
            {


                var notifications = await _repo.GetAllNotificationsByCustomerIdAndNotificationType(user.Id, notificationType);

                var notificationsDto = notifications.Select(n => new NotificationGetDTO
                {
                    Message = n.Message
                }).ToList();
                //Why do we call .ToList() ?
                //Because Select() returns an IEnumerable, not a list.
                //.ToList() forces the query to execute NOW, and materializes the results into:
                //List <NotificationGetDTO>

                return notificationsDto;
            }
        }

        public async Task<List<NotificationForAdmin>> GetAllNotificationsByWorkshop(ClaimsPrincipal userPrincipal)
        {
            var user = await userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }
            var notifications = await _repo.GetAllNotificationsByWorkshopId(user.Id);
            var notificationsDto = notifications.Select(n => new NotificationForAdmin
            {
                message = n.Message,
                type=n.Type,
                date=n.SendingDate,
                customerId =n.CustomerId
            }).ToList();
            return notificationsDto;
        }

        public async Task<List<NotificationGetDTO>> GetAllNotificationsByCustomerToken(ClaimsPrincipal userPrincipal, NotificationTypeEnum? notificationType)
        {
            var user = await userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }
            
            var notifications=await _repo.GetNotificationForUser(user.Id, notificationType);
            
            var notificationsDto = notifications.Select(n => new NotificationGetDTO
            {
                Message = n.Message,
            }).ToList();
            return notificationsDto;
        }
    }
}
