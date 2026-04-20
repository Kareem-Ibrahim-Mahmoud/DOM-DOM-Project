using QudraSaaS.Application.DTOs.SessionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interface
{
    public interface ISessionService
    {
        public Task<CreateSessionDto> CreateSession(ClaimsPrincipal userPrincipal,CreateSessionDto createSessionDto);
        public Task<GetSessionDto> GetSessionById(int id);
        public Task<List<GetSessionDto>> GetSessionsforCar(int carId);
        public Task<List<GetSessionDto>> GetSessionsforUser(string userId);
        public Task<List<GetSessionDto>> GetSessionsforWorkshop(string WorkshopId);
        public Task<bool> updateSession(int SessionId, UpdateSessionDto sessionDto);

        public Task<bool> Delete(int id);
    }
}
