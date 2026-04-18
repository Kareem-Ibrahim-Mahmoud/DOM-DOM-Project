using QudraSaaS.Dmain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace QudraSaaS.Infrastructure
{
    public class Context: IdentityDbContext<applicationUser>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //local Database
            optionsBuilder.UseSqlServer(@"Data source=.;Initial catalog=DOMDOMSAAS;Integrated security=true;TrustServerCertificate=true;");

            //Monster
            //optionsBuilder.UseSqlServer(@"Server=db37099.public.databaseasp.net; Database=db37099; User Id=db37099; Password=8Gw=!e7CpB?2; Encrypt=False; MultipleActiveResultSets=True;");

            //Monster Payed
           // optionsBuilder.UseSqlServer(@"Server=db38299.public.databaseasp.net; Database=db38299; User Id=db38299; Password=gS-8X=3e9h#M; Encrypt=False; MultipleActiveResultSets=True;");


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }



        public DbSet<Customer> Customers { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<ServiceSession> ServiceSessions { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Notification> Notifications {  get; set; }
   
        public DbSet<OTPCode> OTPs { get; set; }

        public DbSet<TransferRequest> transferRequests { get; set; }
        public DbSet<Offers> offers { get; set; }
        public DbSet<OliType> oliTypes { get; set; }
        public DbSet<FCM> fcms { get; set; }

    }

}
