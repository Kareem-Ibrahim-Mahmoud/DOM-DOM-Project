using QudraSaaS.Application.DTOs;
using QudraSaaS.Application.DTOs.User;
using QudraSaaS.Application.DTOs.workshop;
using QudraSaaS.Dmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interface
{
    public interface IUser
    {
        public Task<bool> AddUser(RegesterCustmer registerUserDto,ClaimsPrincipal userPrincipal);
        public Task<GetUserDto> GetbyId(string id);
        public Task<string> loginUser(LoginCustumerDTO loginDto);
        public Task<bool> logout(string DeviceId);
        public Task<string> RegesterWorkshop(RegesterWorkshop regesterWorkshop);
        public Task<string> loginWorkshop(LoginWorkshopDTO loginDto);
        public Task<GetWorkshopDto> GetWorkshop(ClaimsPrincipal userPrincipal);
        public Task<List<GetUserDto>> GetUsersForWorkshop(ClaimsPrincipal userPrincipal);
        public Task<UserHomePage> GetUserHomePage(ClaimsPrincipal userPrincipal);
        public Task<object> GetUsersByName(ClaimsPrincipal userPrincipal, string name);
        public Task<UpdateUserDto> UpdateUser(string userId, UpdateUserDto updateUser);
        public Task<bool> DeleteUser(string userId);
        public Task<WorkshopPorfile> WorkshopPorfile(ClaimsPrincipal userPrincipal);
        public Task<workshopHomepage> workshopHomepage(ClaimsPrincipal userPrincipal);
        public Task<object> WorkshopHomePage2(ClaimsPrincipal userPrincipal);
        public Task<object> RanksAdminDto(ClaimsPrincipal userPrincipal);
        public Task<bool> ResetPassword(string userId);
        public Task<object> SearchBar(ClaimsPrincipal userPrincipal, string name);
        public Task<List<GetWorkshopAdmin>> GetAllWorkshop();
    }
}
