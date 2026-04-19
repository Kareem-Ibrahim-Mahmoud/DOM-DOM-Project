using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QudraSaaS.Application.Interface;
using QudraSaaS.Application.IServices;
using QudraSaaS.Application.Repo;
using QudraSaaS.Application.Services;
using QudraSaaS.Dmain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ISessionService, SessionRepository>();
            services.AddScoped<IServiceTypeService, ServiceTypeService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ICarService, CarService>();
   
 
            services.AddScoped<ITransferRequestService,TransferRequestService>();
            services.AddScoped<IOliTypeService, OliTypeService>();
            services.AddScoped<IOffersService, OffersService>();
            services.AddScoped<IAi, Ai>();
            services.AddScoped<ITransferRequestService, TransferRequestService>();
            services.AddScoped<IOTPService, OTPService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IWhatsApp, WhatssApp>();
            services.AddScoped<IAdminRepo, AdminRepo>();
            return services;
        }
    }
}
