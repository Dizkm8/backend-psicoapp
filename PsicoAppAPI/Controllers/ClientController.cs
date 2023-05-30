using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class ClientController : BaseApiController
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;

        }

        /// <summary>
        /// Checks if the user exists in the database and if the entered password matches the one registered in the database
        /// </summary>
        /// <returns>user whose login credentials match</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var user = await _clientService.GetClient(loginUserDto);

            if (user == null) return BadRequest("Invalid credentials");
            var token = _clientService.GenerateToken(user.Id);
            if (string.IsNullOrEmpty(token))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Token generation failed" });
            }
            return Ok(new { Token = token }); // Return the JWT token in the response
        }
    }
}