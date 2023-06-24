using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PsicoAppAPI.DTOs.Appointment;
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
        [HttpGet("get-appointments")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentByUser()
        {
            var isClient = await _service.IsUserClient();
            if (!isClient) return Unauthorized("The user with userId from token are not a valid client");

            var appointments = await _service.GetAppointmentsByUser();
            if (appointments is null) return Unauthorized("The user with userId from token are not a valid client");
            return Ok(appointments);
        }
    }
}