using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.ServiceMediators.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IUserManagementService _userManagementService;

        public AuthController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService ??
                throw new ArgumentNullException(nameof(userManagementService));
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
        /// if not, return a Status 400.
        /// In case of token generation failed return Status 500.
        /// All error returns includes a message.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var user = await _userManagementService.UserService.GetUser(loginUserDto);

            if (user is null) return BadRequest("Invalid credentials");
            var token = await _userManagementService.AuthService.GenerateToken(user.Id);
            if (string.IsNullOrEmpty(token))
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorModel { ErrorCode = 500, Message = "Token generation failed" });
            }
            return Ok(new { Token = token });
        }
    }
}