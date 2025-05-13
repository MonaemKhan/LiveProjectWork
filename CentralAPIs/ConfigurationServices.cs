using CentralAPIs.DBConfiguration;
using CentralAPIs.IRepo;
using CentralAPIs.Repo;
using Microsoft.EntityFrameworkCore;

namespace CentralAPIs
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddAdministrationService(this IServiceCollection services)
        {
            // 🔧 Register all your services here
            services.AddScoped<IUserRepo, UserRepo>();



            // ✔ Register all your services here
            services.AddDbContext<AdminstrationDbContext>(option =>
                    option.UseSqlServer(ConnectionStringList.AdministrationDb));
            return services;
        }
    }
}
