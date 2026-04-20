using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
    public class userRepository: IUserRepository
    {
        private readonly Context context;
        private readonly UserManager<applicationUser> userManager;

        public userRepository(Context context, UserManager<applicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<Customer>getUserById(string id)
        {
            var user= await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new InvalidOperationException("user not found");
            }
            return user;
        }

        public async Task<Customer> GetUserByEmail(string email)
        {
            var user = await context.Customers.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                throw new InvalidOperationException("user not found");
            }
            return user;
        }
        public async Task<bool> UserEsist(string id)
        {
            var user = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new InvalidOperationException("user not found");
            }
            return true;
        }

        public async Task<List<string>> CustomersIdsByRankId(Ranks rank, string workshopid)
        {
            return await context.Customers
                .Where(c => c.rank == rank && c.workShopId == workshopid)
                .Select(c => c.Id)
                .ToListAsync();
        }

        public async Task<bool>UpdateUser(Customer customer)
        {
            context.Customers.Update(customer);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Customer>>GetAllCustomers(string workshopId)
        {
            return await context.Customers
                .Where(x=>x.workShopId==workshopId)
                .OrderBy(x=>x.createdAt)
                .ToListAsync();
        }
        public async Task<List<Customer>> GetUsersByName(string workshopId,string name)
        {
            return await context.Customers.Where(x=>x.workShopId== workshopId&& x.name.Contains(name)).ToListAsync();
        }

        public async Task<bool>UpdateUser(string id, Customer customer)
        {
            var user = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            context.Customers.Update(customer);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var notifications= await context.Notifications
                .Where(n => n.CustomerId == userId)
                .ToListAsync();
            context.Notifications.RemoveRange(notifications);
            // 1️⃣ هات الـ Identity User
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            // 2️⃣ احذف كل الـ Roles المرتبطة به
            var roles = await userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                var removeRolesResult = await userManager.RemoveFromRolesAsync(user, roles);
                if (!removeRolesResult.Succeeded)
                    return false;
            }

            // 3️⃣ احذف الـ Identity User
            var deleteUserResult = await userManager.DeleteAsync(user);
            if (!deleteUserResult.Succeeded)
                return false;

            // 4️⃣ بعد كده احذف الداتا العادية
            var sessions = await context.ServiceSessions
                .Where(x => x.customerId == userId)
                .ToListAsync();
            context.ServiceSessions.RemoveRange(sessions);

            var cars = await context.Cars
                .Where(x => x.customerId == userId)
                .ToListAsync();
            context.Cars.RemoveRange(cars);

            var customer = await context.Customers.FindAsync(userId);
            if (customer != null)
                context.Customers.Remove(customer);

            await context.SaveChangesAsync();
            return true;
        }
        
        public async Task<List<Workshop>> GetAllWorkshop()
        {
            return await context.Workshops.ToListAsync();
        }

        public async Task<int> GetAllCustomers()
        {
            return await context.Customers.CountAsync();
        }

        public async Task<int> GetCustomersCountByWorkshopId(string workshopId)
        {
            return await context.Customers.Where(c => c.workShopId == workshopId).CountAsync();
        }
    }
}
