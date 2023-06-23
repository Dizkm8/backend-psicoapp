using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PsicoAppAPI.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers
{
    public class AppointmentsController : BaseApiController
    {
        private readonly IAppointmentManagementService _service;

        public AppointmentsController(IAppointmentManagementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [Authorize]
        [HttpGet("get-appointment/{userId}")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentByUser(string userId)
        {
            
            return Ok();
        }
    }
}
