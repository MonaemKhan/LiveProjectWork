using Solutaris.InfoWARE.ProtectedBrowserStorage.Extensions;

namespace CommonUI.Configuration
{
    public static class ConfigurationServices
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration config)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
        }


        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }



        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<HttpService>();
            services.AddScoped<ISessionService, SessionService>();


            services.AddServerSideBlazor();
            services.AddIWProtectedBrowserStorage(); // ✅ Required
        }

        public static void ConfigureJsonNamingConvention(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        }
    }
}
