using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain.IRepositories
{
    public interface ICarRepository
    {
        public Task<Car> GetCarById(int carId);
        public Task<bool> CreateCar(Car car);
        public Task<bool> CarEsist(string UserId, string PlatNumber);
        public Task<List<Car>> AllCarsForUser(string UserId);
        public Task<Car> GetById(int id);
        public Task<bool> UpdateCar(Car car);
        public Task<bool> deleteCar(int id);
        public Task<bool> DeleteCarsWithUserId(string UserId);
    }
}
