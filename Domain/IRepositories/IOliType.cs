using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain.IRepositories
{
    public interface IOliType
    {
        public Task<bool> Create(OliType oliType);
        public  Task<bool> Update(OliType oliType);
        public  Task<OliType> GetById(int id);
        public  Task<bool> Delet(int id);
        public Task<List<OliType>> getallbyWorkshop(string workshopId);
    }
}
