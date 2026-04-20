using QudraSaaS.Application.DTOs.RankDTOs;
using QudraSaaS.Dmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.IServices
{
    public interface IRankService
    {
        Task<Rank> Create(ClaimsPrincipal userPrincipal, RankCreateDTO dto);
        Task<List<RankUpdateDTO>> GetAllByWorkshopId(ClaimsPrincipal userPrincipal);
        Task<RankUpdateDTO> GetById(int rankId);

        Task<Rank> Update(RankUpdateDTO dto , int rankId);
        Task<Rank> Delete(int rankId);

    }
}
