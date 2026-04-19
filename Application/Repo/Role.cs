using Microsoft.AspNetCore.Identity;
using QudraSaaS.Application.Interface;


namespace QudraSaaS.Application.Repo
{
    public class Role: IRole
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public Role(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public RoleManager<IdentityRole> RoleManager { get; }

        public async Task<bool> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                throw new InvalidOperationException ("Role name is required");


            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
                throw new InvalidOperationException("Role already exists");

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
                return true;

            return false;
        }

        public async Task<bool> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                throw new InvalidOperationException("Role does not exist");
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return true;
            return false;
        }

        public async Task<List<IdentityRole>> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }


    }
}
