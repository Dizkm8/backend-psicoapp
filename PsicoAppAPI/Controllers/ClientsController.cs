using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs.Validations;
using PsicoAppAPI.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers;

public class ClientsController : BaseApiController
{
    private readonly IClientManagementService _service;

    public ClientsController(IClientManagementService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [Authorize(Roles = "3")]
    [HttpPost("add-appointment/{specialistUserId}")]
    public async Task<ActionResult> GenerateAppointment(string specialistUserId, [FromQuery]
        [Required]
        [MinutesSecondsEqualsZero(
            ErrorMessage = "StartTime cannot have minutes, seconds, milliseconds or microseconds different from 0")]
        [DateWithinEightWeeks(ErrorMessage =
            "StartTime must be equal to or less than 8 weeks from monday's current week")]
        DateTime dateTime)
    {
        // Check if the userId provided match with a specialist enabled
        var isSpecialist = await _service.IsUserSpecialist(specialistUserId);
        if(!isSpecialist) return NotFound("The userId provided do not match with an enabled specialist");

        // Then check if the specialist have the availability requested
        var isSpecialistAvailable = await _service.IsSpecialistAvailable(specialistUserId, dateTime);
        if(!isSpecialistAvailable) return BadRequest("The specialist is not available at the specified time");

        var isUserEnabled = await _service.IsUserEnabled();
        if(!isUserEnabled) return Unauthorized("The user are not enabled to do this action");
        
        

        
        return Ok();
    }
}
