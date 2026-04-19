using QudraSaaS.Application.DTOs.OliTypeDTO;
using QudraSaaS.Dmain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interface
{
    public interface IOliTypeService
    {
        public  Task<GETOliTypeDTO> Getbyid(int id);
        public  Task<bool> Create(OliTypeCreateDTO oliTypeCreateDTO, ClaimsPrincipal userPrincipal);
        public  Task<bool> Delet(int id);
        public  Task<List<GetAllByworkshop>> GetallbyWorkshop(ClaimsPrincipal userPrincipal);
    }
}
