using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.Services.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IUserManagementService _userManagementService;

        public AuthController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService ?? 
                throw new System.ArgumentNullException(nameof(userManagementService));
        }

        /// <summary>
        /// Login the user if the credentials match and return a JWT token with the user's id and role.
        /// </summary>
        /// <param name="loginUserDto">
        /// Id: User's identifier
        /// Password: User's password
        /// </param>
        /// <returns>
        /// JWT Token with id and role if credentials match
        /// The JWT Contains the UserId and the Role Number of the user
        /// The Role Id currently go from 1 to 3, where 1 is Admin 2 is Client and 3 is Specialist.
        /// if the credentials do not match, return a Status 400.
        /// In case of token generation failed return Status 500.
        /// All error returns includes a message.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var user = await _userManagementService.GetUser(loginUserDto);

            if (user is null) return BadRequest("Invalid credentials");
            var token = await _userManagementService.GenerateToken(user.Id);
            if (string.IsNullOrEmpty(token))
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorModel { ErrorCode = 500, Message = "Token generation failed" });
            }
            return Ok(new { Token = token });
        }
    }
}