using Microsoft.EntityFrameworkCore;
using QudraSaaS.Dmain;
using QudraSaaS.Dmain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Infrastructure.Repositories
{
    public class OliTypeRepository: IOliType
    {
        private readonly Context context;
        public OliTypeRepository(Context context) { this.context = context; }

        public async Task<bool>Create(OliType oliType)
        {
            context.oliTypes.Add(oliType);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool>Update(OliType oliType)
        {
            context.oliTypes.Update(oliType);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<OliType>GetById(int id)
        {
            var oli=await context.oliTypes.FirstOrDefaultAsync(o => o.Id == id);
            if(oli == null)
            {
                throw new InvalidOperationException($"Could not find OliType with id : {id}");
            }
            return oli;

        }

        public async Task<bool>Delet(int id)
        {
            var oli = await context.oliTypes.FirstOrDefaultAsync(o => o.Id == id);
            if (oli == null)
            {
                throw new InvalidOperationException($"Could not find OliType with id : {id}");
            }
            context.oliTypes.Remove(oli);
            await context.SaveChangesAsync();
            return true;

        }
        public async Task<List<OliType>> getallbyWorkshop(string workshopId)
        {
            return await context.oliTypes
                .Where(x => x.workshopId == workshopId)
                .ToListAsync();
        }

    }
}
