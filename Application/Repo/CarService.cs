using QudraSaaS.Application.DTOs.CarDTO;
using QudraSaaS.Application.Interface;
using QudraSaaS.Dmain;
using QudraSaaS.Dmain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Repo
{
    public class CarService: ICarService
    {
        private readonly IUserRepository userRepository;
        private readonly ICarRepository carRepository;

        public CarService(IUserRepository userRepository,ICarRepository carRepository)
        {
            this.userRepository = userRepository;
            this.carRepository = carRepository;
        }
        public async Task<GetCarDto>GetCar(int id)
        {
            var Car = await carRepository.GetCarById(id);
            
            
                var carDto = new GetCarDto
                {
                    Id = Car.Id,
                    customerId = Car.customerId,
                    CarModel = Car.CarModel,
                    CurrentKm = Car.CurrentKm,
   
 
                    nextChange = Car.nextChange,
                    LastChange = Car.LastChange,
 
                    Year = Car.Year,
                    OilType = Car.OilType,
                    PlateNumber = Car.PlateNumber,
                    Make = Car.Make,

                };
                
            return carDto;
        }

        public async Task<bool>CrateCar(CreateCarDto dto)
        {
            //chesk User Esist
            if (!await userRepository.UserEsist(dto.customerId))
            {
                throw new InvalidOperationException("User Not Found");
            }
            //chesk car Esist (User Id,PlateNumber)
            if(await carRepository.CarEsist(dto.customerId, dto.PlateNumber))
            {
                throw new InvalidOperationException("This car is already available");
            }

            Car car = new Car
            {
                customerId = dto.customerId,
                CarModel=dto.CarModel,
                CurrentKm = dto.CurrentKm,
                Year=dto.Year,
                OilType=dto.OilType,
                PlateNumber = dto.PlateNumber,
                Make=dto.Make,
            };
            if(await carRepository.CreateCar(car)) { return true; }
            return false;
        }

        public async Task<List<GetCarDto>>GetAllCarsForUser(string UserId)
        {
            var Cars=await carRepository.AllCarsForUser(UserId);
            var CarsDto=new List<GetCarDto>();
            foreach (var Car in Cars)
            {
                var carDto=new GetCarDto
                {
                    Id=Car.Id,
                    customerId = Car.customerId,
                    CarModel = Car.CarModel,
                    CurrentKm = Car.CurrentKm,
                    Year = Car.Year,
                    OilType = Car.OilType,
   
 
                    nextChange = Car.nextChange,
                    LastChange = Car.LastChange,
 
                    PlateNumber = Car.PlateNumber,
                    Make = Car.Make,
                }; 
                CarsDto.Add(carDto);
            }
            return CarsDto;
        }

        public async Task<bool>Update(int id, UpdateCarDto dto)
        {
            Car car = await carRepository.GetById(id);

            if (dto.CarModel != null) { car.CarModel = dto.CarModel; }
            if (dto.Make != null) { car.Make = dto.Make; }
            if (dto.CurrentKm != null) { car.CurrentKm = dto.CurrentKm.Value; }
            if (dto.OilType != null) { car.OilType = dto.OilType; }
            if (dto.Year != null) { car.Year = dto.Year.Value; }
            if (dto.PlateNumber != null) { car.PlateNumber = dto.PlateNumber; }

            if(await carRepository.UpdateCar(car)) { return true; }
            return false;
        }

        public async Task<bool>DeleteCar(int id)
        {
            if(await carRepository.deleteCar(id)) { return true; }
            return false;
        }
    }
}
