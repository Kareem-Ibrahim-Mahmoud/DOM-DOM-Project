using QudraSaaS.Application.DTOs.CarDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interface
{
    public interface ICarService
    {
        public Task<bool> CrateCar(CreateCarDto dto);
        public Task<List<GetCarDto>> GetAllCarsForUser(string UserId);
        public Task<bool> Update(int id, UpdateCarDto dto);
        public Task<bool> DeleteCar(int id);
        public Task<GetCarDto> GetCar(int id);
    }
}
