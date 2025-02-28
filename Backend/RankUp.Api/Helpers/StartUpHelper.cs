using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RanklUpp.Infrastructure.Context;
using RanklUpp.Infrastructure.Repositories;
using RankUpp.Api.Configurations;
using RankUpp.Application.Interfaces.Repositories;
using RankUpp.Application.Interfaces.Services;
using RankUpp.Application.Services;
using RankUpp.Core.Exceptions;
using System.Text;

namespace RankUpp.Api.Helpers
{
    public static class StartUpHelper
    {
        public static IServiceCollection AddBackendServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IMemoryRepository, MemoryRepository>();
            services.AddScoped<IMemoryService, MemoryService>();

            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            return services;
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RankUppDbContext>(option => option.UseNpgsql(GetConnectionString(configuration)));

            return services;
        }

        public static IServiceCollection AddBearerSecurityToSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RankUpp",
                    Version = "v1",
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });

                c.OperationFilter<AddAuthorizationHeaderOperationFilter>();
            });

            return services;
        }

        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(RankUpp.Application.Mappers.QuizProfile).Assembly);
            services.AddAutoMapper(typeof(RankUpp.Application.Mappers.MemoryMapper).Assembly);

            return services;
        }

        public static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(StartUpHelper.GetTokenSecretKey(configuration))),
                    };
                });

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

        private static string GetTokenSecretKey(IConfiguration configuration)
        {
            var secretKey = Environment.GetEnvironmentVariable("TOKEN_KEY");

            if (secretKey != null)
            {
                return secretKey;
            }
            secretKey =  configuration.GetValue<string>("JwtSettings:TokenKey");

            if (secretKey == null)
            {
                throw new AppSettingNotFoundException();
            }

            return secretKey;
        }
    }
}
