using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Login the user if the credentials match and return a JWT token with the user's id and role
        /// </summary>
        /// <returns>JWT Token with id and role if credentials match,
        /// if not return a Status 400.
        /// In case of token generation failed return Status 500.
        /// All error returns include a message.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var user = await _userService.GetUser(loginUserDto);

            if (user == null) return BadRequest("Invalid credentials");
            var token = await _userService.GenerateToken(user.Id);
            if (string.IsNullOrEmpty(token))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Token generation failed" });
            }
            return Ok(new { Token = token });
        }

        // /// <summary>
        // /// Get all users in database context
        // /// </summary>
        // /// <returns>All users collected</returns>
        // [HttpGet]
        // public IActionResult GetUsers()
        // {
        //     return Ok(_userRepository.GetUsers());
        // }

        // /// <summary>
        // /// Checks if the user exists in the database and if the entered password matches the one registered in the database
        // /// </summary>
        // /// <returns>user whose login credentials match</returns>
        // [AllowAnonymous]
        // [HttpPost("login")]
        // public IActionResult Login([FromBody] LoginModelDto loginModelDto)
        // {
        //     var user = _userRepository.GetUserByCredentials(loginModelDto.Id, loginModelDto.Password);

        //     if (user == null) return Unauthorized(); // Maybe we could change Unauthorized to NotFound here?
        //     if (user.Id == null) return NotFound();

        //     var token = GenerateJwtToken(user.Id);

        //     return Ok(new { Token = token }); // Return the JWT token in the response
        // }

        // /// <summary>
        // /// Add a user in database context if user's id is not registered in the database
        // /// </summary>
        // /// <param name="user">User to add</param>
        // /// <returns>User saved</returns>
        // [HttpPost("add-client-non-admin")]
        // public async Task<ActionResult> AddClient(RegisterClientDto clientDto)
        // {
        //     var userExists = await _userRepository.UserExists(clientDto.Id);
        //     if (userExists)
        //     {
        //         return Conflict(new
        //         {
        //             message = "User already exists.",
        //             userId = clientDto.Id,
        //         });
        //     }
        //     var client = new Client()
        //     {
        //         Id = clientDto.Id,
        //         Name = clientDto.Name,
        //         FirstLastName = clientDto.FirstLastName,
        //         SecondLastName = clientDto.SecondLastName,
        //         Password = clientDto.Password,
        //         Email = clientDto.Email,
        //         Gender = clientDto.Gender,
        //         IsEnabled = true,
        //         Phone = clientDto.Phone,
        //         IsAdministrator = false,
        //     };
        //     await _userRepository.AddClientAndSaveChanges(client);
        //     return Ok(clientDto);
        // }

        // [HttpPost("add-specialist")]
        // public async Task<ActionResult> CreateSpecialist(RegisterSpecialistDto specialistDto)
        // {
        //     // // Check if the specified SpecialityId exists
        //     // var existingSpeciality = await _userRepository.GetUserById(specialistDto.SpecialityId);
        //     // if (existingSpeciality == null)
        //     // {
        //     //     return BadRequest("Invalid SpecialityId. Please provide a valid SpecialityId.");
        //     // }
        //     var userExists = await _userRepository.UserExists(specialistDto.Id);
        //     if (userExists)
        //     {
        //         return Conflict(new
        //         {
        //             message = "User already exists.",
        //             userId = specialistDto.Id,
        //         });
        //     }

        //     // Map the properties from the DTO to the Specialist entity
        //     var specialist = new Specialist
        //     {
        //         Name = specialistDto.Name,
        //         FirstLastName = specialistDto.FirstLastName,
        //         SecondLastName = specialistDto.SecondLastName,
        //         Id = specialistDto.Id,
        //         Email = specialistDto.Email,
        //         Gender = specialistDto.Gender,
        //         Phone = specialistDto.Phone,
        //         Password = specialistDto.Password,
        //         SpecialityId = specialistDto.SpecialityId,
        //     };
        //     await _userRepository.AddSpecialistAndSavechanges(specialist);
        //     return Ok(specialistDto);
        // }


        // private string GenerateJwtToken(string userId)
        // {
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes(_jwtSecret);
        //     var tokenDescriptor = new SecurityTokenDescriptor
        //     {
        //         Subject = new ClaimsIdentity(new[]
        //         {
        //             new Claim(ClaimTypes.Name, userId)
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