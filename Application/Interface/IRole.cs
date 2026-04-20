using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interface
{
    public interface IRole
    {
        public Task<bool> CreateRole(string roleName);
        public Task<bool> DeleteRole(string roleName);
        public Task<List<Microsoft.AspNetCore.Identity.IdentityRole>> GetAllRoles();
    }
}
