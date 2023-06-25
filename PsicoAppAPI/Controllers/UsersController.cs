using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs.UpdateProfileInformation;
using PsicoAppAPI.DTOs.User;
using PsicoAppAPI.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserManagementService _service;

        public UsersController(IUserManagementService userManagementService)
        {
            _service = userManagementService ??
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
            var profileInformationDto = await _service.GetUserProfileInformation();
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
        public async Task<ActionResult> UpdateProfileInformation(
            [FromBody] UpdateProfileInformationDto updateProfileInformationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Needs to validate if exists In Different users to avoid
            // rejecting the update if the user doesn't change the email
            var existsEmail = await _service.CheckEmailUpdatingAvailability(updateProfileInformationDto);
            if (existsEmail) return BadRequest(new { error = "Email already exists" });

            var result = await _service.UpdateProfileInformation(updateProfileInformationDto);
            if (result is null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { error = "Internal error updating User" });
            return Ok(result);
        }

        /// <summary>
        /// Get all the users in the system
        /// </summary>
        /// <returns>
        /// If the user Id from the token doesn't match with an admin or specialist return 401 Unauthorized
        /// If the system have no users return an empty list
        /// If the system have users return a list with UserDto, this have the following structure:
        /// Id: User's identifier
        /// IsEnable: boolean about if it is enabled
        /// RoleName: Name of the role the user have
        /// Email: User's email, must be not null
        /// Gender: User's gender, must be not null and have 8 digits
        /// 
        /// The next three attributes are used to show the user's full name
        /// I suggest threat like "private" stuff, so they are not used in the client side
        /// use fullName attribute instead
        /// UserName: Post's user name 
        /// UserFirstLastName: Post's user first last name
        /// UserSecondLastName: Post's user second last name
        /// 
        /// FullName: Name, first last name and second last name of the user
        /// </returns>
        [Authorize(Roles = "1, 3")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var isAdminOrSpecialist = await _service.IsAdminOrSpecialist();
            if (!isAdminOrSpecialist) return Unauthorized("The user with userId from token are not a valid user");

            var users = await _service.GetAllUsers();
            return Ok(users);
        }

        /// <summary>
        /// Get all the users in the system
        /// </summary>
        /// <returns>
        /// If the user Id from the token doesn't match with an admin or client return 401 Unauthorized
        /// If the system have no users return an empty list
        /// If the system have specialist users return a list with SpecialistDto, this have the following structure:
        /// UserId: User's identifier
        /// UserIsEnable: boolean about if it is enabled
        /// UserRoleName: Name of the role the user have
        /// UserEmail: User's email, must be not null
        /// UserGender: User's gender, must be not null and have 8 digits
        /// 
        /// The next three attributes are used to show the user's full name
        /// I suggest threat like "private" stuff, so they are not used in the client side
        /// use fullName attribute instead
        /// UserName: Post's user name 
        /// UserFirstLastName: Post's user first last name
        /// UserSecondLastName: Post's user second last name
        /// 
        /// UserFullName: Name, first last name and second last name of the user
        /// SpecialityName: Name of the speciality of the specialist
        /// </returns>
        [Authorize(Roles = "1, 2")]
        [HttpGet("get-all-specialists")]
        public async Task<ActionResult<IEnumerable<SpecialistDto>>> GetAllSpecialists()
        {
            var isAdminOrClient = await _service.IsAdminOrClient();
            if (!isAdminOrClient)
                return Unauthorized("The user with userId from token are not a valid admin or client");

            var specialists = await _service.GetAllSpecialists();
            return Ok(specialists);
        }

        /// <summary>
        /// Get a specialist by their UserId
        /// </summary>
        /// <param name="userId">UserId of the specialist</param>
        /// <returns>
        /// If the user Id from the token doesn't match with an admin or client return 401 Unauthorized
        /// If the system have no user with the provided user Id return status code 400 BadRequest with custom message
        /// If the system have specialist user return a SpecialistDto with the follow structure:
        /// UserId: User's identifier
        /// UserIsEnable: boolean about if it is enabled
        /// UserRoleName: Name of the role the user have
        /// UserEmail: User's email, must be not null
        /// UserGender: User's gender, must be not null and have 8 digits
        /// 
        /// The next three attributes are used to show the user's full name
        /// I suggest threat like "private" stuff, so they are not used in the client side
        /// use fullName attribute instead
        /// UserName: Post's user name 
        /// UserFirstLastName: Post's user first last name
        /// UserSecondLastName: Post's user second last name
        /// 
        /// UserFullName: Name, first last name and second last name of the user
        /// SpecialityName: Name of the speciality of the specialist
        /// </returns>
        [Authorize(Roles = "1, 2")]
        [HttpGet("get-specialist/{userId}")]
        public async Task<ActionResult<SpecialistDto>> GetSpecialistByUserId(string userId)
        {
            var isAdminOrClient = await _service.IsAdminOrClient();
            if (!isAdminOrClient)
                return Unauthorized("The user with userId from token are not a valid admin or client");

            var specialist = await _service.GetSpecialistByUserId(userId);
            if (specialist is null) return BadRequest($"The specialist with userId = {userId} do not exists");
            return Ok(specialist);
        }
    }
}