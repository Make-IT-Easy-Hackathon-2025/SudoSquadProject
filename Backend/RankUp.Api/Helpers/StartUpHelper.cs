using Microsoft.EntityFrameworkCore;
using RanklUpp.Infrastructure.Context;
using RankUpp.Core.Exceptions;

namespace RankUpp.Api.Helpers
{
    public static class StartUpHelper
    {
        public static IServiceCollection AddBackendServices(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RankUppDbContext>(option => option.UseNpgsql(GetConnectionString(configuration)));

            return services;
        }


        private static string GetConnectionString(IConfiguration configuration)
        {
            string? connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            if (connectionString != null)
            {
                return connectionString;
            }
            connectionString = configuration.GetConnectionString("RankUppDatabase1");

            if(connectionString == null)
            {
                throw new AppSettingNotFoundException();
            }

            return connectionString;
        }
    }
}
