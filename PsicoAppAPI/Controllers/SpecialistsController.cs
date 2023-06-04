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
        [HttpGet("availability")]
        public async Task<ActionResult<List<AvailabilitySlotDto>?>> GetScheduleAvailability()
        {
            var slots = _service.GetAvailabilitySlots();
            return Ok(slots);
        }
    }
}