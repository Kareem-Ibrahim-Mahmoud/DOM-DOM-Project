using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain.IRepositories
{
    public interface IRankRepository
    {
        Task<Rank> Create(Rank rank);
        Task<List<Rank>> GetAllByWorkshopId(string workshopId);
        Task<Rank> GetById(int rankId);
        Task<Rank> Update(Rank rank);
        Task<Rank> Delete(Rank rank);
        Task<bool> IsExists(string workshopId, string name);
    }
}
