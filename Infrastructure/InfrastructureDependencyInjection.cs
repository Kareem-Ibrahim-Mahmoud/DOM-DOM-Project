using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QudraSaaS.Dmain;
using QudraSaaS.Dmain.IRepositories;
using QudraSaaS.Infrastructure.loges;
using QudraSaaS.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, userRepository>();
            services.AddScoped<IWorkshopRepository, WorkshopRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ISessionRepository,SessionRepository>();
            services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
   
 
            services.AddScoped<ITransferRequest, TransferRequestRepository>();
            services.AddScoped<IOliType, OliTypeRepository>();
            services.AddScoped<IOfferRepository, OfferRepository>();
            services.AddScoped<IFCMRepository, FCMRepository>();
            services.AddScoped<ILoggerService, SerilogLoggerService>();
            return services;
        }

    }
}
