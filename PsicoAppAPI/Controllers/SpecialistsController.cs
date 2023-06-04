using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs.Specialist;
using PsicoAppAPI.Services.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class SpecialistsController : BaseApiController
    {
        private ISpecialistManagementService _service;

        public SpecialistsController(ISpecialistManagementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }


        [Authorize(Roles = "3")]
        [HttpGet("availability/{dateTime}")]
        public async Task<ActionResult<List<AvailabilitySlotDto>?>> GetScheduleAvailability([Required] DateOnly date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var slots = _service.GetAvailabilitySlots(date);
            return Ok(slots);
        }

    }
}