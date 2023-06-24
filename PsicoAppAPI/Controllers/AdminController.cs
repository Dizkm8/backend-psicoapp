using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs.User;
using PsicoAppAPI.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers;

public class AdminController : BaseApiController
{
    private readonly IAdminManagementService _service;

    public AdminController(IAdminManagementService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [Authorize(Roles = "1")]
    [HttpGet]
    public async Task<ActionResult<string>> GetGptRules()
    {
        var isAdmin = await _service.IsUserAdmin();
        if (!isAdmin) return Unauthorized("The user with userId from token are not a valid admin");

        var rule = await _service.GetModerationRules();
        if (rule is null)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal error getting moderation rules" });
        return Ok(rule);
    }

    [Authorize(Roles = "1")]
    //TODO: Change to HttpPut
    [HttpPost("update-rules")]
    public async Task<ActionResult<string>> SetGptRules(
        [Required] [StringLength(800, ErrorMessage = "Rules cannot be larger than 255 characters.")]
        string rules)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var isAdmin = await _service.IsUserAdmin();
        if (!isAdmin) return Unauthorized("The user with userId from token are not a valid admin");

        var result = await _service.SetModerationRules(rules);
        if (result) return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError,
            new { error = "Internal error updating moderation rules" });
    }

    /// <summary>
    /// Create a new specialist
    /// </summary>
    /// <param name="specialistDto">
    /// Id: User's identifier, must be unique and not null or empty
    /// Name: User's name, must be not null and have at least 2 characters
    /// FirstLastName: User's first last name, must be not null and have at least 2 characters
    /// SecondLastName: User's second last name, must be not null and have at least 2 characters
    /// Email: User's email, must be not null, have a valid email format and be unique
    /// Gender: User's gender, must be not null or empty
    /// Phone: User's phone, must be not null and have 8 digits
    /// Password: User's password, mut be not null and have a length between 10 and 15 characters
    /// ConfirmPassword: User's password, mut be not null and have a length between 10 and 15 characters and the same as Password
    /// SpecialityId: Id of the speciality of the specialist
    /// </param>
    /// <returns>
    /// If the ModelState have errors based on params requirements, return a status code 400 with the errors.
    /// If the Email exists, Id exists or specialityId do not exists, return a status code 400 with the errors (can return all of them at the same time).
    /// If the user cannot be added to the database, return a status code 500 with a custom error.
    /// If the user is added to the database, return a status code 200 with no message
    /// </returns>
    [Authorize(Roles = "1")]
    [HttpPost("create-specialist")]
    public async Task<ActionResult> CreateSpecialist([FromBody] RegisterSpecialistDto specialistDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existsEmail = await _service.CheckEmailAvailability(specialistDto);
        if (existsEmail) ModelState.AddModelError("Email", "Email already exists");

        var existsId = await _service.CheckUserIdAvailability(specialistDto);
        if (existsId) ModelState.AddModelError("Id", "Id already exists");

        var existsSpeciality = await _service.ExistsSpeciality(specialistDto);
        if (!existsSpeciality) ModelState.AddModelError("SpecialityId", "Speciality Id do not exists");
        // Return errors if exists  
        if (!ModelState.IsValid) return BadRequest(ModelState);


        var result = await _service.AddSpecialist(specialistDto);
        if (!result)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal error creating a new specialist" });
        return Ok();
    }
}