using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QudraSaaS.Application.DTOs;
using QudraSaaS.Application.DTOs.SessionDto;
using QudraSaaS.Application.DTOs.User;
using QudraSaaS.Application.Interface;
using QudraSaaS.Dmain;
using QudraSaaS.Dmain.Enums;
using QudraSaaS.Dmain.IRepositories;
using QudraSaaS.Infrastructure;
using QudraSaaS.Application.DTOs;
using QudraSaaS.Dmain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using QudraSaaS.Application.Interface;
using QudraSaaS.Application.DTOs.workshop;
using QudraSaaS.Application.DTOs.PasswordDto;


namespace QudraSaaS.Application.Repo
{
    public class UserRepo: IUser
    {
        private readonly UserManager<applicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly Context context;
        private readonly IHttpContextAccessor _httpContextAccessor; 
        private readonly IUserRepository userRepository;
        private readonly IWorkshopRepository workshopRepository;
        private readonly ISessionRepository sessionRepository;
        private readonly ICarRepository carRepository;
        private readonly INotificationRepository notificationRepository;
        private readonly IWhatsApp whatsApp;
        private readonly IServiceTypeRepository serviceTypeRepository;
        private readonly IFCMRepository fCMRepository;

        public UserRepo(UserManager<applicationUser> userManager, IConfiguration config, Context context, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository,IWorkshopRepository workshopRepository, ISessionRepository sessionRepository,ICarRepository carRepository, INotificationRepository notificationRepository,IWhatsApp whatsApp, IServiceTypeRepository serviceTypeRepository, IFCMRepository fCMRepository)
        {
            _userManager = userManager;
            _config = config;
            this.context = context;
            _httpContextAccessor = httpContextAccessor; 
            this.userRepository = userRepository;
            this.workshopRepository = workshopRepository;
            this.sessionRepository = sessionRepository;
            this.carRepository = carRepository;
            this.notificationRepository = notificationRepository;
            this.whatsApp = whatsApp;
            this.serviceTypeRepository = serviceTypeRepository;
            this.fCMRepository = fCMRepository;
        }

        public async Task<bool>AddUser(RegesterCustmer registerUserDto,ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);

            if (user == null) { 
                throw new InvalidOperationException("Workshop user not found.");
            }
            var exists = await context.Customers.AnyAsync(u => u.PhoneNumber == registerUserDto.phone);
            if (exists)
            { 
                throw new InvalidOperationException("The Number Already exists");
            }
            var existss = await context.Customers.AnyAsync(u => u.whats == registerUserDto.whats);
            if (existss)
            {
                throw new InvalidOperationException("The whatsApp Number Already exists");
            }
            Customer app = new Customer();
            app.name = registerUserDto.name;
            app.phone = registerUserDto.phone;
            app.Email = registerUserDto.Email;
            app.whats = registerUserDto.whats;
            app.rank= Ranks.عادي;
            app.UserName =Guid.NewGuid().ToString();
            app.createdAt = DateTime.Now;
            app.workShopId = user.Id;
            
            app.notes = registerUserDto.notes;  
            

            IdentityResult result = await _userManager.CreateAsync(app, registerUserDto.phone);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(app,"Customer");

            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Error(s) occurred while registering the account: {errors}");
            }

            Car car = new Car()
            {
                CarModel = registerUserDto.CarModel,
                Make = registerUserDto.Make,
                Year = registerUserDto.Year,
                PlateNumber = registerUserDto.PlateNumber,
                CurrentKm = registerUserDto.CurrentKm,
                OilType = registerUserDto.OilType,
                customerId = app.Id
            };
            if(!await carRepository.CreateCar(car))
            {
                throw new InvalidOperationException("The user was created successfully, but there was a problem creating the car.");
            }
            string message = $"👋 أهلاً وسهلاً يا {registerUserDto.name}\r\n\r\nتم تسجيلك بنجاح على تطبيق Zabtly 🚗🔧\r\n\r\nتقدر تتابع كل صيانات عربيتك، المواعيد، والأسعار بسهولة من الموبايل.\r\n\r\n🔐 بيانات تسجيل الدخول:\r\n📱 الرقم: {registerUserDto.phone}\r\n🔑 كلمة المرور: {registerUserDto.phone}\r\n\r\n📲 ادخل على التطبيق من هنا:\r\nhttps://play.google.com/store/apps/details?id=com.qudra.zabtly\r\n\r\nلو محتاج أي مساعدة، إحنا دايمًا معاك 💙\r\n\r\nفريق Zabtly";
            
            await whatsApp.SendOtpAsync("+2"+registerUserDto.whats, message);

            return true;

        }
        public async Task<GetUserDto>GetbyId(string id)
        {
            var user = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                throw new InvalidOperationException($"The User Id:{id} are not found");
            GetUserDto custmerDTO = new GetUserDto();
            custmerDTO.phone = user.phone;
            custmerDTO.email = user.Email;
            custmerDTO.rank = user.rank;
            custmerDTO.name = user.name;
            custmerDTO.whats = user.whats;
            custmerDTO.notes = user.notes;
            custmerDTO.numOfSession = user.numberOfVisits;
       
            return custmerDTO;

        }
        
        public async Task<string> loginUser(LoginCustumerDTO loginDto)
        {
            var work = await context.Customers.FirstOrDefaultAsync(u => u.phone == loginDto.phone);
            if (work == null)
            {
                throw new InvalidOperationException("The User is not registered.");
            }
            bool passwordValid = await _userManager.CheckPasswordAsync(work, loginDto.Password);
            if (!passwordValid)
                throw new InvalidOperationException("phone or password are invalid");
            var roles = await _userManager.GetRolesAsync(work);
            
            if (!roles.Contains("Customer"))
                throw new InvalidOperationException("You are not a user");

            var claims = new List<Claim>
            {
                new Claim("phone", work.phone),
                new Claim(ClaimTypes.NameIdentifier, work.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudiance"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: creds
            );

            #region generate FCM token
            var fcm = new FCM
            {
                Token = loginDto.FCMToken,
                UserId = work.Id,
                DeviceId = loginDto.DeviceId
            };
            await fCMRepository.AddToken(fcm); 
            #endregion


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<bool> logout(string DeviceId)
        {
            await fCMRepository.RemoveTokenByDeviceId(DeviceId);
            return true;
        }
        public async Task<string> RegesterWorkshop(RegesterWorkshop regesterWorkshop)
        {
            var exists = await context.Workshops.AnyAsync(u => u.phone == regesterWorkshop.phone);
            if (exists)
            {
                throw new InvalidOperationException("The Number Already exists");
            }
            var existss = await context.Workshops.AnyAsync(u => u.whats == regesterWorkshop.whats);
            if (existss)
            {
                throw new InvalidOperationException("The whats App Number Already exists");
            }
            string code;
            do
            {
                code = new Random().Next(100000, 999999).ToString();
            }
            while (await context.Workshops.AnyAsync(u => u.code == code));

            Workshop app = new Workshop();
            app.name = regesterWorkshop.name;
            app.phone = regesterWorkshop.phone;
            app.address = regesterWorkshop.address;
            app.whats = regesterWorkshop.whats;
            app.UserName = Guid.NewGuid().ToString();
            app.workingHours=regesterWorkshop.workingHours;
            app.code = code;
            IdentityResult result = await _userManager.CreateAsync(app, regesterWorkshop.phone);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(app, "Workshop");
                return code;
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Error(s) occurred while registering the account: {errors}");
            }
            
        }
        public async Task<string> loginWorkshop(LoginWorkshopDTO loginDto)
        {
            var work = await context.Workshops.FirstOrDefaultAsync(
                u => u.phone==loginDto.phone&&u.code==loginDto.code);
            if (work == null)
            {
                throw new InvalidOperationException("phone number or code or password invaled");
            }
            
            bool passwordValid = await _userManager.CheckPasswordAsync(work, loginDto.password);
            if (!passwordValid)
                throw new InvalidOperationException("phone number or code or password invaled");
            
            var roles = await _userManager.GetRolesAsync(work);

            
            var claims = new List<Claim>
            {
                new Claim("phone", work.phone),
                new Claim(ClaimTypes.NameIdentifier, work.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudiance"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<GetWorkshopDto> GetWorkshop(ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }
            Customer userr = await userRepository.getUserById(user.Id);
            Workshop workshop = await workshopRepository.GetWorkshopById(userr.workShopId);
            var workshopDto = new GetWorkshopDto();
            
            workshopDto.whats = workshop.whats;
            workshopDto.address = workshop.address;
            workshopDto.PhoneNumber = workshop.phone;
            workshopDto.name = workshop.name;
            workshopDto.workingHours = workshop.workingHours;

            var serviceTypes = await serviceTypeRepository.GetAllByWorkshopId(workshop.Id);
            
            if (serviceTypes != null)
            {
                foreach (var item in serviceTypes)
                { 
                    workshopDto.serviceTypes.Add(item.Name);
                }
            }
            return workshopDto;

        }

        public async Task<List<GetUserDto>> GetUsersForWorkshop(ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }
            var users=await userRepository.GetAllCustomers(user.Id);
            var usersDto = new List<GetUserDto>();
            foreach (var item in users)
            {
                var userDto = new GetUserDto();
                userDto.Id = item.Id;
                userDto.name = item.name;
                userDto.phone = item.phone;
                userDto.whats = item.whats;
                userDto.notes = item.notes;
                userDto.email = item.Email;
                userDto.rank = item.rank;
                userDto.numOfSession =item.numberOfVisits ;
                usersDto.Add(userDto);
            }
            return usersDto;
        }

        public async Task<UserHomePage> GetUserHomePage(ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }
            var userr = await userRepository.getUserById(user.Id);

            var userHomePage = new UserHomePage();
            userHomePage.Rank = userr.rank.ToString();
            userHomePage.numberOfSessions = userr.numberOfVisits;
            var sessions = await sessionRepository.GetSessionsforUser(userr.Id);

            List<GetSessionDto> lastSessions = new List<GetSessionDto>();
            foreach(var session in sessions)
            {
                lastSessions.Add(new GetSessionDto
                {
                    id = session.id,
                    date = session.date,
                    AdditionalServices = session.AdditionalServices,
                    cost = session.cost,
                });
                
            }

            userHomePage.nextChange = sessions.Count > 0
                ? (sessions[0].NextChange ?? 0)
                : 0;
            if(lastSessions.Count>0) userHomePage.lastSession.Add(lastSessions[0]);
            if (lastSessions.Count > 1) userHomePage.lastSession.Add(lastSessions[1]);


            return userHomePage;
        }
        
        public async Task<object> GetUsersByName(ClaimsPrincipal userPrincipal, string name)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }

            List<Customer> users = await userRepository.GetUsersByName(user.Id,name);

            var userDtos = users.Select(user => new
            {
                Id = user.Id,
                Name = user.name
            }).ToList();
            return userDtos;
        }

        public async Task<UpdateUserDto> UpdateUser(string userId, UpdateUserDto updateUser)
        {
            var user = await userRepository.getUserById(userId);
            
            if(updateUser.whats!=null) { user.whats = updateUser.whats;}
            if(updateUser.Name!=null) { user.name = updateUser.Name; }
            if(updateUser.Email!=null) {user.Email= updateUser.Email; }
            if (!string.IsNullOrEmpty( updateUser.phone)) { user.phone = updateUser.phone; }
            
            await userRepository.UpdateUser(userId,user);
            return updateUser;
            
        }
        
        public async Task<bool>DeleteUser(string userId)
        {
            await userRepository.DeleteUser(userId);
            await carRepository.DeleteCarsWithUserId(userId);
            await sessionRepository.DeleteServiceSessionWithUserId(userId);
            await notificationRepository.DeleteNotificationWithUserId(userId);
            
            return true;
        }
    
        public async Task<WorkshopPorfile> WorkshopPorfile(ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }
            Workshop workshop = await workshopRepository.GetWorkshopById(user.Id);
            var workshopDto = new WorkshopPorfile
            {
                name=workshop.name,
                workingHours=workshop.workingHours,
                address=workshop.address,
                phone=workshop.phone,

            };
            return workshopDto;
        }
    
        public async Task<workshopHomepage> workshopHomepage(ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }
            return await workshopRepository.WorkshopHomepage(user.Id);
        }

        public async Task<object> WorkshopHomePage2(ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }
            return await workshopRepository.WorkshopHomePage2(user.Id);
        }

        public async Task<object> RanksAdminDto(ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }
            var count= await workshopRepository.RanksAdminDto(user.Id);

            var lastOffers=await workshopRepository.listOffers(user.Id);
            return new
            {
                RankDetails= count,
                lastOffers = lastOffers
            };
        }
        
        public async Task<bool>ChangePassword(ClaimsPrincipal userPrincipal, ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }
            var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, changePasswordDto.CurrentPassword);
            if (!isCurrentPasswordValid)
            {
                throw new InvalidOperationException("Current password is incorrect.");
            }
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Error(s) occurred while changing the password: {errors}");
            }
            return true;
        }
        
        public async Task<bool>ResetPassword(string userId)
        {
            var user = await userRepository.getUserById(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }
            string password = "1234567899";
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, password); //resetPasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Error(s) occurred while resetting the password: {errors}");
            }
            return true;
        }

        public async Task<object> SearchBar(ClaimsPrincipal userPrincipal, string name)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("invaled token");
            }

            List<Customer> users = await userRepository.GetUsersByName(user.Id, name);

            var userDtos = users.Select(user => new
            {
                Id = user.Id,
                Name = user.name,
                phone=user.phone,
                numberOfVisits = user.numberOfVisits,
                rank= user.rank
            }).ToList();
            return userDtos;
        }

        public async Task<List<GetWorkshopAdmin>> GetAllWorkshop()
        {
            var workshops=await userRepository.GetAllWorkshop();
            var workshopsDto=new List<GetWorkshopAdmin>();
            foreach(var workshop in workshops)
            {
                workshopsDto.Add(new GetWorkshopAdmin
                {
                    Id = workshop.Id,
                    PhoneNumber = workshop.phone,
                    name=workshop.name,
                    code=workshop.code,
                    address=workshop.address,
                    workingHours=workshop.workingHours,
                    whats=workshop.whats,
                });
            }
            return workshopsDto;
        }

    }
}
