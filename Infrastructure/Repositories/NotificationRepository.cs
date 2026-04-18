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
    public class NotificationRepository : INotificationRepository
    {
        private readonly Context _context;
        private readonly IUserRepository _customerRepo;

        public NotificationRepository(Context context , IUserRepository customerRepo)
        {
            _context = context;
            _customerRepo = customerRepo;
        }

        public async Task<bool> Create(List<Notification> notifications)
        {
            await _context.AddRangeAsync(notifications);
            _context.SaveChanges();
            return true;
        }
        public async Task<bool> CreateOne(Notification notification)
        {
            await _context.AddAsync(notification);
            _context.SaveChanges();
            return true;
        }

        public async Task<List<Notification>> GetAllNotificationsByCustomerId(string customerId)
        {
           
            var customer = await _customerRepo.getUserById(customerId);

            if(customer == null)
            {
                return new List<Notification>(); // empty list or throw an exception
                // return Null here not recommended 
                // If your repository returns List<Notification>, then returning null violates the expectation.
                // A list should always be a list — even if empty.
            }

            var notifications = await _context.Notifications.Where(n => n.CustomerId == customerId).ToListAsync();

            return notifications;
            
            
        }

        public async Task<List<Notification>> GetAllNotificationsByCustomerIdAndNotificationType(string customerId, NotificationTypeEnum notificationType)
        {

            var customer = await _customerRepo.getUserById(customerId);

            if (customer == null)
            {
                return new List<Notification>(); // empty list or throw an exception
               
            }
            var notifications = await _context.Notifications.Where(n => n.CustomerId == customerId && n.Type == notificationType).ToListAsync();

            return notifications;
        }

        public async Task<List<Notification>> GetAllNotificationsByWorkshopId(string workshopId)
        {
            var notifications = await _context.Notifications.Where(n => n.WorkshopId == workshopId).ToListAsync();
            return notifications;
        }

        public async Task<bool>SendNotificationsToList(List<string>usersId, Notification notificationTemplate)
        {
            List<Notification> notifications = new List<Notification>();
            foreach (var userId in usersId)
            {
                Notification notification = new Notification
                {
                    Message = notificationTemplate.Message,
                    Type = notificationTemplate.Type,
                    SendingDate = DateTime.UtcNow,
                    WorkshopId = notificationTemplate.WorkshopId,
                    CustomerId = userId
                };
                notifications.Add(notification);
            }
            await Create(notifications);
            return true;
        }
   
        public async Task<List<Notification>>GetNotificationForUser(string userId, NotificationTypeEnum? notificationType)
        {
            if (notificationType == NotificationTypeEnum.all)
            {
                var notifications = await _context.Notifications
                .Where(x => x.CustomerId == userId).ToListAsync();
                return notifications;
            }
            else
            {
                var notifications = await _context.Notifications
                 .Where(x => x.CustomerId == userId && x.Type == notificationType).ToListAsync();
                return notifications;
            }
        }

        public async Task<bool> DeleteNotificationWithUserId(string UserId)
        {
            var cars = await _context.Cars.Where(c => c.customerId == UserId).ToListAsync();
            _context.RemoveRange(cars);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
