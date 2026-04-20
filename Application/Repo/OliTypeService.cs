using Microsoft.AspNetCore.Identity;
using QudraSaaS.Application.DTOs.OliTypeDTO;
using QudraSaaS.Application.Interface;
using QudraSaaS.Dmain;
using QudraSaaS.Dmain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Repo
{
    public class OliTypeService: IOliTypeService
    {
        private readonly IOliType oliType;
        private readonly UserManager<applicationUser> _userManager;

        public OliTypeService(IOliType oliType, UserManager<applicationUser> userManager)
        {
            this.oliType = oliType;
            _userManager = userManager;
        }

        public async Task<GETOliTypeDTO>Getbyid(int id)
        {
            var oli=await oliType.GetById(id);
            var olidto = new GETOliTypeDTO 
            {
                Id = oli.Id,
                oiltybe=oli.oiltybe,
                KM=oli.KM        
            };
            return olidto;
        }

        public async Task<bool>Create(OliTypeCreateDTO oliTypeCreateDTO, ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);

            if (user == null)
            {
                throw new InvalidOperationException("Workshop user not found.");
            }

            var oli = new OliType
            {
                oiltybe = oliTypeCreateDTO.oiltybe,
                KM = oliTypeCreateDTO.KM,
                workshopId=user.Id
            };
            
            if(await oliType.Create(oli))
            {
                return true;
            }
            return false;

        }

        public async Task<bool>Delet(int id)
        {
            if(await oliType.Delet(id)) { return true; } return false;
        }

        public async Task<List<GetAllByworkshop>> GetallbyWorkshop(ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);

            if (user == null)
            {
                throw new InvalidOperationException("Workshop user not found.");
            }

            var oli=await oliType.getallbyWorkshop(user.Id);
            var ol = new List<GetAllByworkshop>();
            foreach(var o in oli)
            {
                var olidto =new GetAllByworkshop
                {
                    workshopId = o.workshopId,
                    KM = o.KM,
                    oiltybe=o.oiltybe,
                    Id = o.Id

                };
                ol.Add(olidto);
            }
            return ol;
        }



    }
}
