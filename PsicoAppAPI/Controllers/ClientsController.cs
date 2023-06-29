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

    /// <summary>
    /// Add an appointment for a client to the system
    /// </summary>
    /// <param name="specialistUserId">User Id of the specialist to request appointment</param>
    /// <param name="dateTime">Date and time formatting in ISO 8601 of the appointment</param>
    /// <returns>
    /// If the model state of the Datetime are not valid return status code 400 BadRequest with the modelState message
    /// If the specialist userId do not exists return a status code 404 NotFound with custom message
    /// If the client userId are not enabled return a status code 401 Unauthorized with custom message
    /// If the specialist are not available in that Date and time return a status code 400 BadRequest with custom message
    /// If something went wrong adding the appointment return a status code 500 Internal Server Error with custom message
    /// If everything goes well return status code 200 with custom message.
    /// </returns>
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
        if (!ModelState.IsValid) return BadRequest(ModelState);

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

    /// <summary>
    /// Send a message to GPT and wait for their response
    /// </summary>
    /// <param name="message">Content of the message</param>
    /// <returns>
    /// If the user are not enabled or do not exists in the system return status code  401 Unauthorized
    /// with custom message
    /// If something went wrong connecting to GPT api or server side error return status code 500 Internal Server Error
    /// with custom message
    /// If everything goes well return a Dto with the content of the response, only have this attribute: 
    /// Content: The content of the response
    /// </returns>
    [Authorize]
    [HttpPost("chat")]
    public async Task<ActionResult> ChatWithBoth([FromBody] SimpleMessageDto message)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var isEnabled = await _service.IsUserEnabled();
        if (!isEnabled) return Unauthorized("The user do not exists or are not enabled");

        var response = await _service.ChatWithBot(message);
        if (response is null)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal error chatting" });
        return Ok(response);
    }

    /// <summary>
    /// Get all the chat messages of the user order from the older to the newer
    /// </summary>
    /// <returns>
    /// If the user are not enabled or do not exists in the system return status code  401 Unauthorized
    /// with custom message
    /// If something went wrong getting all the messages return status code 500 Internal Server Error
    /// with custom message
    /// If everything goes well return a list of MessageDto with the following structure:
    /// Content: The content of the message
    /// SendOn: The Date and time of the message, follow standard ISO8601
    /// IsBotAnswer: a boolean which means if is a GPT response (true) or was a user sent message (false)
    ///
    /// If the user has no chats return an empty list
    /// </returns>
    [Authorize]
    [HttpGet("get-chat")]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetAllChatMessages()
    {
        var isEnabled = await _service.IsUserEnabled();
        if (!isEnabled) return BadRequest("The user do not exists or are not enabled");

        var chat = await _service.GetChat();
        if (chat is null)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal error getting chat" });
        return Ok(chat);
    }
}