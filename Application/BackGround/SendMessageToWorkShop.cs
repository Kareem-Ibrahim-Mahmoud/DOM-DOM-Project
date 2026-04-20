using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QudraSaaS.Application.Interface;
using QudraSaaS.Dmain.Enums;
using QudraSaaS.Dmain.IRepositories;
using QudraSaaS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.BackGround
{
    public class SendMessageToWorkShop: BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IWhatsApp whatsApp;
        private readonly IWorkshopRepository workshopRepository;

        public SendMessageToWorkShop(IServiceScopeFactory scopeFactory, IWhatsApp whatsApp,IWorkshopRepository workshopRepository)
        {
            this.scopeFactory = scopeFactory;
            this.whatsApp = whatsApp;
            this.workshopRepository = workshopRepository;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckSubscriptions();

                // يستنى 24 ساعة
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }

        private async Task CheckSubscriptions()
        {
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Context>();

            var start = DateTime.UtcNow.Date.AddDays(1);
            var end = start.AddDays(1);

            var workshops = await context.Workshops
                .Where(w => w.SubstitutionDate >= start &&
                            w.SubstitutionDate < end)
                .ToListAsync();

            foreach (var workshop in workshops)
            {
                // هنا تبعت الرسالة
                string message = $"👋 أهلاً وسهلاً يا {workshop.name}\r\n\r\nحابين نفكرك إن اشتراكك في Zabtly هيخلص بكرة ⏳\r\n\r\nعلشان تفضل مستمتع بكل مميزاتنا \r\n\r\nيرجى تجديد الاشتراك قبل ميعاد الانتهاء ✅\r\n\r\n💳 تقدر تجدد بسهولة من خلال التواصل مع 01557711290.\r\n\r\nلو محتاج أي مساعدة، إحنا دايمًا معاك 💙\r\n\r\nفريق Zabtly 🚗🔧";
                await whatsApp.SendOtpAsync("+2" + workshop.whats, message);
            }

            var expiredWorkshops = await context.Workshops
                .Where(w => w.EndDate < DateTime.Now&&w.state==WorkshopState.Active)
                .ToListAsync();
            foreach (var workshop in expiredWorkshops)
            {
                // هنا تبعت الرسالة
                string message = $"👋 أهلاً وسهلاً يا {workshop.name}\r\n\r\nحابين نفكرك إن اشتراكك في Zabtly انتهى ⏳\r\n\r\nعلشان تفضل مستمتع بكل مميزاتنا \r\n\r\nيرجى تجديد الاشتراك لتجنب انقطاع الخدمة ✅\r\n\r\n💳 تقدر تجدد بسهولة من خلال التواصل مع 01557711290.\r\n\r\nلو محتاج أي مساعدة، إحنا دايمًا معاك 💙\r\n\r\nفريق Zabtly 🚗🔧";
                await whatsApp.SendOtpAsync("+2" + workshop.whats, message);
                workshop.state = WorkshopState.Inactive;
                
                await workshopRepository.UpdateWorkshop(workshop);

            }
        }

    }
}
