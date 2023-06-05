using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.Specialist;
using PsicoAppAPI.Services.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class SpecialistsController : BaseApiController
    {
        private readonly ISpecialistManagementService _service;

        public SpecialistsController(ISpecialistManagementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Get the specialists availibities of a week from the given date
        /// The Date is a DateOnly c# class, which follows the yyyy-mm-dd format
        /// Requires role 3 (Specialist)
        /// </summary>
        /// <param name="date">Date of the week to return</param>
        /// <returns>
        /// If the date provided is not in the right format or is not summoned, return
        /// error 400 BadRequest with the ModelState errors.
        /// If the date is not in the allowed range, return error 404 NotFound with a message.
        /// The range is between the current week and the next 8 weeks.
        /// If something goes wrong getting the avaialbility (UserId in token don't exists,
        /// error in availabilities in server, etc.) return error 500 InternalServerError with a message.
        /// If the specialist have none availabilities in the given week, return an empty list.
        /// If everything goes well, return the availabilities of the week in the given date.
        /// In a List shaped as AvailabilitySlotDto, the attributes are:
        /// StartTime: DateTime c# class, follows ISO8601 format.
        /// IsAvailable: bool, true if the specialist is available at that time, false otherwise.
        /// </returns>
        [Authorize(Roles = "3")]
        [HttpGet("availability/{date}")]
        public async Task<ActionResult<IEnumerable<AvailabilitySlotDto>?>> GetScheduleAvailability([Required] DateOnly date)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_service.ValidateDate(date)) return NotFound(
                new ErrorModel { ErrorCode = 404, Message = "The date provided are not in the allowed range" });

            var slots = await _service.GetAvailabilitySlots(date);
            if (slots is null) return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal error getting availability" });
            return Ok(slots);
        }

        /// <summary>
        /// Add the availabilities provided in the body of the request.
        /// The availabilities are a list of AddAvailabilityDto, which have the following attributes:
        /// StartTime: DateTime c# class, follows ISO8601 format. The date must be equal to or later than the current date.
        /// Requires role 3 (Specialist)
        /// </summary>
        /// <param name="availabilities">List of availabilities to add</param>
        /// <returns>
        /// If the dates provided are not in the right format or are not summoned, return
        /// error 400 BadRequest with the ModelState errors.
        /// If the dates are not in the allowed range, return error 400 BadRequest with a message.
        /// The range is between the current week and the next 8 weeks.
        /// If the hours provided are not in the right format, return error 400 BadRequest with a message.
        /// If the hours are not in the allowed range, return error 400 BadRequest with a message.
        /// The range is between 8:00 and 20:00.
        /// If something goes wrong adding the availabilities (UserId in token don't exists,
        /// error in availabilities in server, etc.) return error 500 internal server error
        /// with a message
        /// If everything goes well, return the availabilities added.
        /// </returns>
        [Authorize(Roles = "3")]
        [HttpPost("add-availability")]
        public async Task<ActionResult> AddScheduleAvailability(IEnumerable<AddAvailabilityDto> availabilities)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var validateDates = _service.ValidateDateOfAvailabities(availabilities);
            if (!validateDates) return BadRequest(
                new ErrorModel { ErrorCode = 400, Message = "The dates provided are not in the allowed range" });

            var validateHours = _service.ValidateTimeOfAvailabities(availabilities);
            if (!validateHours) return BadRequest(
                new ErrorModel { ErrorCode = 400, Message = "The hours provided are not in the allowed range" });

            var result = await _service.AddSpecialistAvailability(availabilities);
            if (result is null) return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Could not add the availabilities" });
            return Ok(result);
        }

    }
}