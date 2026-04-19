using Microsoft.AspNetCore.Identity;
using QudraSaaS.Application.DTOs.SessionDto;
using QudraSaaS.Application.Interface;
using QudraSaaS.Dmain;
using QudraSaaS.Dmain.Enums;
using QudraSaaS.Dmain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Repo
{
    public class SessionRepository: ISessionService
    {
        private readonly UserManager<applicationUser> userManager;
        private readonly ICarRepository carRepo;
        private readonly IUserRepository userRepo;
        private readonly ISessionRepository sessionRepo;
        private readonly IOliType oliRepo;
        private readonly IUserRepository userRepository;

        public SessionRepository(UserManager<applicationUser> userManager, ICarRepository carRepo, IWorkshopRepository workshopRepo, IUserRepository userRepo, ISessionRepository sessionRepo, IOliType oliRepo, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.carRepo = carRepo;
            WorkshopRepo = workshopRepo;
            this.userRepo = userRepo;
            this.sessionRepo = sessionRepo;
            this.oliRepo = oliRepo;
            this.userRepository = userRepository;
        }

        public IWorkshopRepository WorkshopRepo { get; }

        public async Task<CreateSessionDto>CreateSession(ClaimsPrincipal userPrincipal, CreateSessionDto createSessionDto)
        {
            var AppUser = await userManager.GetUserAsync(userPrincipal);
            if (AppUser == null)
            {
                throw new InvalidOperationException("invaled token");
            }
           
            // check Date(User,workshop,car)
            var user= await userRepo.getUserById(createSessionDto.customerId);
            var workshop= await WorkshopRepo.GetWorkshopById(AppUser.Id);
            var car= await carRepo.GetCarById(createSessionDto.carId);

            var session = new ServiceSession();
            session.UserName = user.name;
            session.customerId = user.Id;
            session.carId = car.Id;
            session.workShopId = workshop.Id;
            session.date = createSessionDto.sessionDate;
            session.CarModel = car.CarModel;
            session.OilChanged = createSessionDto.OilChanged;
            session.FilterChanged = createSessionDto.FilterChanged;
            session.description = createSessionDto.description;
            session.cost = createSessionDto.cost;
            session.AdditionalServices = createSessionDto.AdditionalServices;
            
            if (createSessionDto.OilChanged)
            {
                if (createSessionDto.OilId == null)
                {
                    throw new InvalidOperationException("Oil Type is required when oil is changed");
                }
                var oil = await oliRepo.GetById(createSessionDto.OilId.Value);
                
                session.KmReading = createSessionDto.KmReading?? throw new InvalidOperationException("KmReading لا يمكن أن يكون فارغ");
                session.OilId = createSessionDto.OilId?? throw new InvalidOperationException("OilId لا يمكن أن يكون فارغ");
                session.NumberOfKilometers = createSessionDto.NumberOfKilometers?? throw new InvalidOperationException("NumberOfKilometers لا يمكن أن يكون فارغ");
                session.NextChange = createSessionDto.NextChange?? throw new InvalidOperationException("NextChange لا يمكن أن يكون فارغ");
                session.OilType = oil.oiltybe;
                car.nextChange = ((int)oil.KM);
                
                
            
            }
            #region update user rank
            user.numberOfVisits++;
            if (user.numberOfVisits > 0 && user.numberOfVisits < 5) { user.rank = Ranks.عادي; }
            else if (user.numberOfVisits >= 5 && user.numberOfVisits < 10) { user.rank = Ranks.برونزي; }
            else if (user.numberOfVisits >= 10 && user.numberOfVisits < 15) { user.rank = Ranks.فضي; }
            else if (user.numberOfVisits >= 15 && user.numberOfVisits < 20) { user.rank = Ranks.ذهبي; }
            else if (user.numberOfVisits >= 20) { user.rank = Ranks.البلاتيني; }
            await userRepo.UpdateUser(user.Id, user);
            #endregion
            //go to DB to save 
            if (!await sessionRepo.CreateSession(session))
            {
                throw new InvalidOperationException("A problem occurred while storing data.");
            }
            
            car.LastChange=DateTime.Now;

            await carRepo.UpdateCar(car);
            return createSessionDto;
        }
    
        public async Task<GetSessionDto>GetSessionById(int id)
        {
            //get session from Db
            var session=await sessionRepo.GetSessionById(id);
            var sessionDto=new GetSessionDto ();
            //map
            sessionDto.id=id;
            sessionDto.description= session.description;
            sessionDto.cost= session.cost;
            sessionDto.date=session.date;
            sessionDto.CarModel=session.CarModel;
            sessionDto.FilterChanged=session.FilterChanged;
            sessionDto.KmReading= session.KmReading;
            sessionDto.NumberOfKilometers = session.NumberOfKilometers;
            sessionDto.workShopId= session.workShopId;
            sessionDto.AdditionalServices= session.AdditionalServices;
            sessionDto.NextChange= session.NextChange;
            sessionDto.UserName= session.UserName;
            sessionDto.OilChanged= session.OilChanged;
            sessionDto.carId= session.carId;
            sessionDto.OilId= session.OilId;
            sessionDto.OilType= session.OilType;
            return sessionDto;
        }
        
        public async Task<List<GetSessionDto>> GetSessionsforCar(int carId)
        {
            var car= await carRepo.GetCarById(carId);
            if(car==null)
            {
                throw new InvalidOperationException("Car not found");
            }

            //get data form DB
            var sessions = await sessionRepo.GetSessionsforCar(carId);
            var sessionsDto=new List<GetSessionDto>();
            foreach (var session in sessions)
            {
                var sessionDto = new GetSessionDto();
                //map
                sessionDto.id = session.id;
                sessionDto.description = session.description;
                sessionDto.cost = session.cost;
                sessionDto.date = session.date;
                sessionDto.CarModel = session.CarModel;
                sessionDto.FilterChanged = session.FilterChanged;
                sessionDto.KmReading = session.KmReading;
                sessionDto.NumberOfKilometers = session.NumberOfKilometers;
                sessionDto.workShopId = session.workShopId;
                sessionDto.AdditionalServices = session.AdditionalServices;
                sessionDto.NextChange = session.NextChange;
                sessionDto.UserName = session.UserName;
                sessionDto.UserId=session.customerId;
                sessionDto.OilChanged = session.OilChanged;
                sessionDto.carId = session.carId;
                sessionDto.OilId = session.OilId;
                sessionDto.OilType = session.OilType;
                sessionsDto.Add(sessionDto);
            }
            return sessionsDto;
        }

        public async Task<List<GetSessionDto>> GetSessionsforUser(string userId)
        {
            //get data form DB
            var sessions = await sessionRepo.GetSessionsforUser(userId);
            var sessionsDto = new List<GetSessionDto>();
            foreach (var session in sessions)
            {
                var sessionDto = new GetSessionDto();
                //map
                sessionDto.id = session.id;
                sessionDto.description = session.description;
                sessionDto.cost = session.cost;
                sessionDto.date = session.date;
                sessionDto.FilterChanged = session.FilterChanged;
                sessionDto.CarModel = session.CarModel;
                sessionDto.KmReading = session.KmReading;
                sessionDto.NumberOfKilometers = session.NumberOfKilometers;
                sessionDto.workShopId = session.workShopId;
                sessionDto.AdditionalServices = session.AdditionalServices;
                sessionDto.NextChange = session.NextChange;
                sessionDto.UserName = session.UserName;
                sessionDto.OilChanged = session.OilChanged;
                sessionDto.carId = session.carId;
                sessionDto.OilId = session.OilId;
                sessionDto.OilType = session.OilType;
                sessionsDto.Add(sessionDto);
            }
            return sessionsDto;
        }

        public async Task<List<GetSessionDto>> GetSessionsforWorkshop(string WorkshopId)
        {
            var sessions = await sessionRepo.GetSessionsforWorkshop(WorkshopId);
            var sessionsDto = new List<GetSessionDto>();
            foreach (var session in sessions)
            {
                var sessionDto = new GetSessionDto();
                //map
                sessionDto.id = session.id;
                sessionDto.description = session.description;
                sessionDto.cost = session.cost;
                sessionDto.date = session.date;
                sessionDto.UserId=session.customerId;
                sessionDto.FilterChanged = session.FilterChanged;
                sessionDto.CarModel = session.CarModel;
                sessionDto.KmReading = session.KmReading;
                sessionDto.NumberOfKilometers = session.NumberOfKilometers;
                sessionDto.workShopId = session.workShopId;
                sessionDto.AdditionalServices = session.AdditionalServices;
                sessionDto.NextChange = session.NextChange;
                sessionDto.UserName = session.UserName;
                sessionDto.OilChanged = session.OilChanged;
                sessionDto.carId = session.carId;
                sessionDto.OilId = session.OilId;
                sessionsDto.Add(sessionDto);
            }
            return sessionsDto;
        }

        public async Task<bool>updateSession(int SessionId,UpdateSessionDto sessionDto)
        {
            
            var session=await sessionRepo.GetSessionById(SessionId);

            session.NextChange = sessionDto.NextChange ?? session.NextChange;
            session.description = sessionDto.description ?? session.description;
            session.KmReading = sessionDto.KmReading ?? session.KmReading;
            session.NumberOfKilometers = sessionDto.NumberOfKilometers ?? session.NumberOfKilometers;
            session.FilterChanged = sessionDto.FilterChanged ?? session.FilterChanged;
            session.AdditionalServices = sessionDto.AdditionalServices ?? session.AdditionalServices;
            session.cost = sessionDto.cost ?? session.cost;
            session.date = sessionDto.sessionDate ?? session.date;
            //session.carId = sessionDto.carId ?? session.carId;
            if (!string.IsNullOrWhiteSpace(sessionDto.userId))
            {
                var user = await userRepository.getUserById(sessionDto.userId);
                session.customerId = user.Id;
                session.UserName = user.name;
            }
            if(sessionDto.carId != null)
            {
                var car= await carRepo.GetCarById(sessionDto.carId.Value);
                session.carId = car.Id;
                session.CarModel = car.CarModel;
            }
            if(sessionDto.OilId != null)
            {
                var oil = await oliRepo.GetById(sessionDto.OilId.Value);
                session.OilId = sessionDto.OilId.Value;
                session.OilType = oil.oiltybe;
            }
            if (!await sessionRepo.UpdateAsync(session))
            {
                throw new InvalidOperationException("A problem occurred while storing data.");
            }
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (await sessionRepo.deleteServiceSession(id)) { return true; }

            return false;
        }


    }
}
