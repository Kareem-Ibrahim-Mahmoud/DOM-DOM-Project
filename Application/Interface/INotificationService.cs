using QudraSaaS.Application.DTOs.NotificationDTOs;
using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.IServices
{
    public interface INotificationService
    {
        Task<bool> Create(ClaimsPrincipal userPrincipal,NotificationDTO dto);
        Task<bool> CreateSingleNotification(ClaimsPrincipal userPrincipal, NotificationForUser dto);
        Task<List<NotificationGetDTO>> GetAllNotificationsByCustomerId(ClaimsPrincipal userPrincipal);
        Task<List<NotificationGetDTO>> GetAllNotificationsByCustomerIdAndNotificationType(ClaimsPrincipal userPrincipal, NotificationTypeEnum notificationType);
        Task<List<NotificationForAdmin>> GetAllNotificationsByWorkshop(ClaimsPrincipal userPrincipal);
        Task<List<NotificationGetDTO>> GetAllNotificationsByCustomerToken(ClaimsPrincipal userPrincipal, NotificationTypeEnum? notificationType);
    }

}
