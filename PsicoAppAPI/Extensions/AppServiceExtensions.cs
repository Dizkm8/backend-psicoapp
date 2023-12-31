using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PsicoAppAPI.Data;
using PsicoAppAPI.Mediators;
using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Repositories;
using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services;
using PsicoAppAPI.Services.Interfaces;
using Swashbuckle.AspNetCore.Filters;

namespace PsicoAppAPI.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services = AddSwaggerGen(services);
            services = AddAutoMapper(services);
            services = AddUnitOfWork(services);
            services = AddEntitiesServices(services);
            services = AddHelperServices(services);
            services = AddMediatorServices(services);
            services = AddData(services, config);
            services = AddAuthentication(services, config);
            services = AddHttpContextAccesor(services);
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

        private static IServiceCollection AddUnitOfWork(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        private static IServiceCollection AddEntitiesServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFeedPostService, FeedPostService>();
            services.AddScoped<IForumPostService, ForumPostService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ISpecialistService, SpecialistService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAppointmentStatusesService, AppointmentStatusesService>();
            services.AddScoped<IChatService, ChatService>();
            return services;
        }

        private static IServiceCollection AddHelperServices(IServiceCollection services)
        {
            services.AddScoped<IOpenAiService, OpenAiService>();
            services.AddScoped<IMapperService, MapperService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITimeZoneService, TimeZoneService>();
            return services;
        }

        private static IServiceCollection AddMediatorServices(IServiceCollection services)
        {
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<IFeedPostManagementService, FeedPostManagementService>();
            services.AddScoped<IForumPostManagementService, ForumPostManagementService>();
            services.AddScoped<ISpecialistManagementService, SpecialistManagementService>();
            services.AddScoped<ITagManagementService, TagManagementService>();
            services.AddScoped<IAuthManagementService, AuthManagementService>();
            services.AddScoped<IClientManagementService, ClientManagementService>();
            services.AddScoped<IAdminManagementService, AdminManagementService>();
            services.AddScoped<IAppointmentManagementService, AppointmentManagementService>();
            return services;
        }

        private static IServiceCollection AddData(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                string connectionString = config.GetConnectionString("DefaultConnection") ?? string.Empty;
                opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
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

        private static IServiceCollection AddHttpContextAccesor(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
