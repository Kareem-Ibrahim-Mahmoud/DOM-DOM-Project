using Microsoft.EntityFrameworkCore;
using QudraSaaS.Dmain;
using QudraSaaS.Dmain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Infrastructure.Repositories
{
    public class CarRepository: ICarRepository
    {
        private readonly Context context;

        public CarRepository(Context context)
        {
            this.context = context;
        }

        public async Task<Car>GetCarById(int carId)
        {
            var car=await context.Cars.FirstOrDefaultAsync(c => c.Id == carId);
            if (car == null)
            {
                throw new InvalidOperationException("Car not found");
            }
            return car;
        }

        public async Task<bool>CreateCar(Car car)
        {
            context.Cars.Add(car);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CarEsist(string UserId, string PlatNumber)
        {
            var car = await context.Cars.FirstOrDefaultAsync(x=>x.customerId == UserId && x.PlateNumber == PlatNumber);
            if (car == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<Car>>AllCarsForUser(string UserId)
        {
            var Cars=await context.Cars.Where(x=>x.customerId == UserId).ToListAsync();
            return Cars;
        }

        public async Task<Car>GetById(int id)
        {
            var car= await context.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if(car == null)
            {
                throw new InvalidOperationException($"Could not find Car with id : {id}");
            }
            return car;
        }

        public async Task<bool>UpdateCar(Car car)
        {
            context.Cars.Update(car);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool>deleteCar(int id)
        {
            var car= await context.Cars.FirstOrDefaultAsync(x=>x.Id==id);
            if (car == null)
            {
                throw new InvalidOperationException($"Could not find Car with id : {id}");
            }
            context.Cars.Remove(car);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool>DeleteCarsWithUserId(string UserId)
        {
            var cars=await context.Cars.Where(c=>c.customerId == UserId).ToListAsync();
            context.RemoveRange(cars);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
