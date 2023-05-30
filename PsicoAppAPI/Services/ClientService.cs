using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class ClientService : IClientService
    {
        // private readonly string _jwtSecret;
        // private readonly IClientRepository _clientRepository;
        // public ClientService(IConfiguration configuration, IClientRepository clientRepository)
        // {
        //     _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        //     var token = configuration.GetValue<string>("JwtSettings:Secret") ??
        //         throw new ArgumentException("JwtSettings:Secret is null");
        //     _jwtSecret = token;
        // }

        // public string? GenerateToken(string? id)
        // {
        //     if (id == null)
        //     {
        //         throw new ArgumentNullException(nameof(id), "User identifier cannot be null");
        //     }
        //     return GenerateJwtToken(id);
        // }


        // public async Task<Client?> GetClient(LoginUserDto loginUserDto)
        // {
        //     if (string.IsNullOrWhiteSpace(loginUserDto.Id) || string.IsNullOrWhiteSpace(loginUserDto.Password)) return null;
        //     var client = await _clientRepository.GetClientByCredentials(loginUserDto.Id, loginUserDto.Password);
        //     return client;
        // }

        // private string GenerateJwtToken(string userId)
        // {
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes(_jwtSecret);
        //     var tokenDescriptor = new SecurityTokenDescriptor
        //     {
        //         Subject = new ClaimsIdentity(new[]
        //         {
        //             new Claim(ClaimTypes.Name, userId),
        //             new Claim(ClaimTypes.Role, "Client")
        //         }),
        //         Expires = DateTime.UtcNow.AddDays(7),
        //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
        //             SecurityAlgorithms.HmacSha256Signature)
        //     };
        //     var token = tokenHandler.CreateToken(tokenDescriptor);
        //     return tokenHandler.WriteToken(token);
        // }
    }
}