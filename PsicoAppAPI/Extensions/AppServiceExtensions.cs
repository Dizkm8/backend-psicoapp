using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PsicoAppAPI.Data;
using PsicoAppAPI.Repositories;
using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.ServiceMediators;
using PsicoAppAPI.ServiceMediators.Interfaces;
using PsicoAppAPI.Services;
using PsicoAppAPI.Services.Interfaces;
using Swashbuckle.AspNetCore.Filters;

namespace PsicoAppAPI.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
        {

            services = AddSwaggerGen(services);
            services = AddAutoMapper(services);
            services = AddRepositories(services);
            services = AddServices(services);
            services = AddServiceMediators(services);
            services = AddData(services, config);
            services = AddAuthentication(services, config);
            services = AddHttpContextAccesor(services);
            return services;
        }

        private static IServiceCollection AddServiceMediators(IServiceCollection services)
        {
            services.AddScoped<IUserManagementService, UserManagementService>();
            return services;
        }

        private static IServiceCollection AddHttpContextAccesor(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            return services;
        }

        private static IServiceCollection AddData(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            return services;
        }

        private static IServiceCollection AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ISpecialistRepository, SpecialistRepository>();
            return services;
        }

        private static IServiceCollection AddSwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            return services;
        }

        private static IServiceCollection AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program).Assembly);
            return services;
        }

        private static IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }

        private static IServiceCollection AddAuthentication(IServiceCollection services, IConfiguration config)
        {
            var jwtSecret = config["JwtSettings:Secret"] ?? throw new Exception("JwtSettings:Secret is null");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return services;
        }
    }
}