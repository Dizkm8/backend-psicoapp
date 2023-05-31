#region CLASS_IMPORTS
using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PsicoAppAPI.Repositories;
using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.Services;
using PsicoAppAPI.Repositories.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Configure swagger to use the security scheme
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

#region AUTOMAPPER_INJECTION
builder.Services.AddAutoMapper(typeof(Program).Assembly);
#endregion

#region REPOSITORIES_INJECTIONS
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ISpecialistRepository, SpecialistRepository>();
#endregion

#region SERVICES_INJECTIONS
builder.Services.AddScoped<IUserService, UserService>();
#endregion

#region DATA_CONTEXT_INJECTION
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region AUTHENTICATION_INJECTION
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Secret"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

#region CORS_CONFIGURATION
app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:5000");
});
#endregion

app.UseHttpsRedirection();

app.MapControllers();

#region SEED_DATABASE
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<DataContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    // Migrate the database, create if it doesn't exist
    context.Database.Migrate();
    Seed.SeedData(context).Wait();
}
catch (Exception ex)
{
    logger.LogError(ex, " A problem ocurred during seeding ");
}
#endregion

app.Run();