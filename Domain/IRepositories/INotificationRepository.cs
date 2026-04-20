using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain.IRepositories
{
    public interface INotificationRepository
    {
        //Task<bool> Create(Notification notification);
        Task<bool> Create(List<Notification> notifications);
        Task<bool> CreateOne(Notification notification);
        Task<List<Notification>>GetAllNotificationsByCustomerId(string customerId);

        Task<List<Notification>> GetAllNotificationsByCustomerIdAndNotificationType(string customerId, NotificationTypeEnum notificationType);

        public Task<bool> SendNotificationsToList(List<string> usersId, Notification notificationTemplate);
        public Task<List<Notification>> GetAllNotificationsByWorkshopId(string workshopId);
        public Task<List<Notification>> GetNotificationForUser(string userId, NotificationTypeEnum? notificationType);
        public Task<bool> DeleteNotificationWithUserId(string UserId);
    }
}
