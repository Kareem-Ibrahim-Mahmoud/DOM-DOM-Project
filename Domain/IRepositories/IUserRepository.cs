using QudraSaaS.Dmain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain.IRepositories
{
    public interface IUserRepository
    {
        public Task<Customer> getUserById(string id);
        public Task<bool> UserEsist(string id);

        public Task<Customer> GetUserByEmail(string email);


        public Task<List<string>> CustomersIdsByRankId(Ranks rank, string workshopid);
        public Task<bool> UpdateUser(Customer customer);
        public Task<List<Customer>> GetAllCustomers(string workshopId);
        public Task<List<Customer>> GetUsersByName(string workshopId,string name);
        public Task<bool> UpdateUser(string userId, Customer customer);
        public Task<bool> DeleteUser(string UserId);
        public Task<List<Workshop>> GetAllWorkshop();
        public Task<int> GetAllCustomers();
        public Task<int> GetCustomersCountByWorkshopId(string workshopId);

    }
}
