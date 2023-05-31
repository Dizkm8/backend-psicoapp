using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
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
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var user = await _userService.GetUser(loginUserDto);

            if (user is null) return BadRequest("Invalid credentials");
            var token = await _userService.GenerateToken(user.Id);
            if (string.IsNullOrEmpty(token))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Token generation failed" });
            }
            return Ok(new { Token = token });
        }

        [AllowAnonymous]
        [HttpPost("register-client")]
        public async Task<ActionResult> RegisterClient([FromBody] RegisterClientDto registerClientDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(new { errors });
            }

            var existsEmail = await _userService.ExistsUserWithEmail(registerClientDto.Email);
            if (existsEmail) ModelState.AddModelError("Email", "Email already exists");

            var existsId = await _userService.ExistsUserById(registerClientDto.Id);
            if (existsId) ModelState.AddModelError("Id", "Id already exists");
            // Return Id or Email duplicated error if exists
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var clientAdded = await _userService.AddClient(registerClientDto);
            if (clientAdded is null) return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal error adding User" });

            return Ok(clientAdded);
        }
    }
}