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


        [Authorize(Roles = "3")]
        [HttpGet("availability/{date}")]
        public async Task<ActionResult<IEnumerable<AvailabilitySlotDto>?>> GetScheduleAvailability([Required] DateOnly date)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_service.ValidateDate(date)) return NotFound(
                new ErrorModel { ErrorCode = 404, Message = "The date provided are not in the allowed range" });

            var slots = await _service.GetAvailabilitySlots(date);
            if (slots is null) return BadRequest();
            return Ok(slots);
        }

        [Authorize(Roles = "3")]
        [HttpPost("add-availability")]
        public async Task<ActionResult> AddScheduleAvailability(IEnumerable<AddAvailabilityDto> availabilities)
        {
            var result = await _service.AddSpecialistAvailability(availabilities);
            if (result is null) return BadRequest(
                new ErrorModel { ErrorCode = 400, Message = "Could not add the availabilities" });
            return Ok(result);
        }

    }
}