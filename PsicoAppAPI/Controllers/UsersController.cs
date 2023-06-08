using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs.UpdateProfileInformation;
using PsicoAppAPI.Services.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserManagementService _userManagementService;

        public UsersController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService ??
                throw new ArgumentNullException(nameof(userManagementService));
        }

        /// <summary>
        /// Get the user's profile information extracting Claims included on the JWT token.
        /// </summary>
        /// <returns>
        /// If the JWT was not provided or is invalid, return a Status 401.
        /// If the JWT have error extracting the Claims, return a Status 404.
        /// If the user's profile information is not found, return a Status 404.
        /// If the user's profile information is found, return a Status 200 with the user's profile information.
        /// The User Profile Information contains:
        /// Id, roleId, name, firstLastName, secondLastName, email, gender and phone. (Check DTO)
        /// </returns>
        [Authorize]
        [HttpGet("profile-information")]
        public async Task<ActionResult<ProfileInformationDto>> GetProfileInformation()
        {
            var profileInformationDto = await _userManagementService.GetUserProfileInformation();
            if (profileInformationDto is null) return Unauthorized("JWT not provided or invalid");
            return Ok(profileInformationDto);
        }

        /// <summary>
        /// Update the user's profile information extracting Claims included on the JWT token.
        /// </summary>
        /// <param name="updateProfileInformationDto">
        /// Name: User's name, must be not null and have at least 2 characters
        /// FirstLastName: User's first last name, must be not null and have at least 2 characters
        /// SecondLastName: User's second last name, must be not null and have at least 2 characters
        /// Email: User's email, must be not null, have a valid email format and be unique
        /// Gender: User's gender, must be not null or empty
        /// Phone: User's phone, must be not null and have 8 digits
        /// </param>
        /// <returns>
        /// If the ModelState have errors based on params requeriments, return a Status 400 with the errors.
        /// If the Email exists in other user, return a Status 400 with the error.
        /// If the user cannot be updated in the database, return a Status 500 with a generic error.
        /// If the user is added to the database, return a Status 200 with the user's data shaped
        ///  as UpdateProfileInformationDto.
        /// </returns>
        [Authorize]
        [HttpPut("profile-information")]
        public async Task<ActionResult> UpdateProfileInformation([FromBody] UpdateProfileInformationDto updateProfileInformationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Needs to validate if exists In Different users to avoid
            // rejecting the update if the user doesn't change the email
            var existsEmail = await _userManagementService.CheckEmailUpdatingAvailability(updateProfileInformationDto);
            if (existsEmail) return BadRequest(new { error = "Email already exists" });

            var result = await _userManagementService.UpdateProfileInformation(updateProfileInformationDto);
            if (result is null) return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal error updating User" });
            return Ok(result);
        }
    }
}