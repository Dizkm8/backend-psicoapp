using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PsicoAppAPI.Repositories;
using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.Services;
using PsicoAppAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Inject ClientRepository to the container as the implementation of IClientRepository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
// Add database context with the configuration in appsettings.json
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Secret"])),
            ValidateIssuer = false, // Ajusta según tus necesidades
            ValidateAudience = false // Ajusta según tus necesidades
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:5000");
});

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
// Seed the database with the json files if the database is empty.
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

app.Run();
