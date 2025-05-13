using CentralAPIs.CustomeActionFilter;
using CentralAPIs.DBConfiguration;
using CentralAPIs.IRepo;
using CentralAPIs.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CentralAPIs
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddAdministrationService(this IServiceCollection services)
        {
            // 🔧 Register all your services here
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ILoginRepo, LoginRepo>();
            services.AddScoped<ISessionRepo, SessionRepo>();



            services.AddScoped<ValidateToken>();


            // ✔ Register all your services here
            services.AddDbContext<AdminstrationDbContext>(option =>
                    option.UseSqlServer(ConnectionStringList.AdministrationDb));
            return services;
        }
    }
}
