using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.Specialist;
using PsicoAppAPI.Mediators.Interfaces;

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
        /// If the dates provided cannot be converted into UTC of Chile (API Dependency transaction)
        /// , return error 500 InternalServerError
        /// If the dates provided are not in the right format, are not summoned,
        /// are not in the valid date range within now and the next 8 weeks,
        /// are not in the valid hour range within 8:00 and 20:00,
        /// then return error 400 BadRequest with modelState errors for each availitibity with error
        /// error 400 BadRequest with the ModelState errors.
        /// If the availabilities provided already exists in the database, return error 400 BadRequest
        /// with a message. The criteria to check if the availabilities exists is the StartTime.
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

            var convertedAvailabilities = await _service.TransformToChileUTC(availabilities);
            if (convertedAvailabilities is null) return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal error adding availabilities" });

            var checkHours = _service.CheckHourRange(convertedAvailabilities);
            if (!checkHours) return BadRequest("One or more availabilities provided are not in the valid hour range");

            var CheckDuplicatedAvailabilities = await _service.CheckDuplicatedAvailabilities(convertedAvailabilities);
            if (CheckDuplicatedAvailabilities) return BadRequest(
                new ErrorModel { ErrorCode = 400, Message = "One or more availabilities provided already exists duplicated" });

            var result = await _service.AddSpecialistAvailability(convertedAvailabilities);
            if (result is null) return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal error adding availabilities" });
            return Ok(result);
        }

    }
}