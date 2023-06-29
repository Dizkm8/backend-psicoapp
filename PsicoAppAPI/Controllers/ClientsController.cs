using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs.Chat;
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

    //TODO: Update this to Roles = "2"
    [Authorize(Roles = "2")]
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
        if (!isSpecialist) return NotFound("The userId provided do not match with an enabled specialist");

        // Then check if the specialist have the availability requested
        var isSpecialistAvailable = await _service.IsSpecialistAvailable(specialistUserId, dateTime);
        if (!isSpecialistAvailable) return BadRequest("The specialist is not available at the specified time");

        var isUserEnabled = await _service.IsUserEnabled();
        if (!isUserEnabled) return Unauthorized("The user are not enabled to do this action");

        var result = await _service.AddAppointment(specialistUserId, dateTime);
        if (result) return Ok("Appointment successfully added");
        return StatusCode(StatusCodes.Status500InternalServerError,
            new { error = "Internal error adding appointment" });
    }

    [Authorize]
    [HttpPost("chat")]
    public async Task<ActionResult> ChatWithBoth([FromBody] SimpleMessageDto message)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var isEnabled = await _service.IsUserEnabled();
        if (!isEnabled) return BadRequest("The user do not exists or are not enabled");

        var response = await _service.ChatWithBot(message);
        if (response is null)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal error chatting" });
        return Ok(response);
    }

    [Authorize]
    [HttpGet("get-chat")]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetAllChatMessages()
    {
        var isEnabled = await _service.IsUserEnabled();
        if (!isEnabled) return BadRequest("The user do not exists or are not enabled");


        return Ok();
    }
}