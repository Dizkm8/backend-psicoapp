using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs;
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

        /// <summary>
        /// Get all the appointmets of an client
        /// </summary>
        /// <returns>
        /// If the userId in the token are not a client and enabled user return status code 401 Unauthorized
        /// If the user has no appointments return status 200 with empty list
        /// If everything goes well return a List with the appointments with status 200 
        /// </returns>
        [Authorize(Roles = "2")]
        [HttpGet("get-appointments")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentByUser()
        {
            var isClient = await _service.IsUserClient();
            if (!isClient) return Unauthorized("The user with userId from token are not a valid client");

            var appointments = await _service.GetAppointmentsByUser();
            if (appointments is null) return Unauthorized("The user with userId from token are not a valid client");
            return Ok(appointments);
        }

        [Authorize(Roles = "1, 2")]
        [HttpDelete("cancel-appointment/{appointmentId:int}")]
        public async Task<ActionResult> CancelAppointment(int appointmentId)
        {
            var isClientOrAdmin = await _service.IsAdminOrClient();
            if (!isClientOrAdmin) return Unauthorized("The user with userId from token are not a valid user");

            var result = await _service.CancelAppointment(appointmentId);
            return result switch
            {
                null => BadRequest("Appointment only can be canceled at least 24 hours before"),
                false => StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorModel { ErrorCode = 500, Message = "Internal error deleting the comment" }),
                true => Ok()
            };
        }
    }
}