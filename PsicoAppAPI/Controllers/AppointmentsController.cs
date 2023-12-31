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
        /// Get all the appointments of an client
        /// </summary>
        /// <returns>
        /// If the userId in the token are not a client and enabled user return status code 401 Unauthorized
        /// If the user has no appointments return status 200 with empty list
        /// If everything goes well return a List with the appointments with status 200
        /// The list of data returned have the following structure:
        ///  Id: Id of the appointment
        /// BookedDate: The Time and Date of the appointment, follows ISO 8601 standard
        ///
        /// /// The next three attributes are used to show the user's full name
        /// I suggest threat like "private" stuff, use RequestedUserFullName attribute instead in the client side
        /// RequestedUserId: The userId of the specialist
        /// RequestedUserName: The name of the specialist
        /// RequestedUserFirstLastName: The first last name of the specialist
        /// RequestedUserSecondLastName: The second last name of the specialist
        ///
        /// 
        /// RequestedUserFullName: The name, first last name and second last name of the specialist
        /// AppointmentStatusName: The status in words (done, booked or canceled)
        /// </returns>
        [Authorize(Roles = "2")]
        [HttpGet("get-appointments-client")]
        public async Task<ActionResult<IEnumerable<ClientAppointmentDto>>> GetAppointmentByClient()
        {
            var isClient = await _service.IsUserClient();
            if (!isClient) return Unauthorized("The user with userId from token are not a valid client");

            var appointments = await _service.GetAppointmentsByClient();
            if (appointments is null) return Unauthorized("The user with userId from token are not a valid client");
            return Ok(appointments);
        }

        [Authorize(Roles = "1")]
        [HttpGet("get-appointments-specialist/{specialistUserId}")]
        public async Task<ActionResult<IEnumerable<SpecialistAppointmentDto>>> GetAppointmentBySpecialist(
            string specialistUserId)
        {
            var isAdmin = await _service.IsAdmin();
            if (!isAdmin) return Unauthorized("The user with userId from token are not a valid specialist");

            var appointments = await _service.GetAppointmentsBySpecialist(specialistUserId);
            if (appointments is null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorModel { ErrorCode = 500, Message = "Internal error getting appointments" });
            return Ok(appointments);
        }

        /// <summary>
        /// Get all the appointments of an specialist
        /// </summary>
        /// <returns>
        /// If the userId in the token are not a specialist and enabled user return status code 401 Unauthorized
        /// If the user has no appointments return status 200 with empty list
        /// If everything goes well return a List with the appointments with status 200
        /// The list of data returned have the following structure:
        ///  Id: Id of the appointment
        /// BookedDate: The Time and Date of the appointment, follows ISO 8601 standard
        ///
        /// /// The next three attributes are used to show the user's full name
        /// I suggest threat like "private" stuff, use RequestedUserFullName attribute instead in the client side
        /// RequestingUserId: The userId of the client
        /// RequestingUserName: The name of the client
        /// RequestingUserFirstLastName: The first last name of the client
        /// RequestingUserSecondLastName: The second last name of the client
        /// 
        /// RequestedUserFullName: The name, first last name and second last name of the client
        /// AppointmentStatusName: The status in words (done, booked or canceled)
        /// </returns>
        [Authorize(Roles = "3")]
        [HttpGet("get-appointments-specialist")]
        public async Task<ActionResult<IEnumerable<SpecialistAppointmentDto>>> GetAppointmentsBySpecialist()
        {
            var isSpecialist = await _service.IsSpecialist();
            if (!isSpecialist) return Unauthorized("The user with userId from token are not a valid specialist");

            var appointments = await _service.GetAppointmentsBySpecialist();
            if (appointments is null) return Unauthorized("The user with userId from token are not a valid specialist");
            return Ok(appointments);
        }

        /// <summary>
        /// Cancel an appointment by user or admin
        /// </summary>
        /// <param name="appointmentId">Id of the appointment to cancel</param>
        /// <returns>
        /// If the userId in the token are not a client and enabled user return status code 401 Unauthorized
        /// with custom message
        /// If the appointment what would be canceled are not in the range of at least 24 hours return a
        /// status code 400 Bad Request with custom message
        /// If something went wrong canceling the appointment return status code 500 Internal error with custom message
        /// If everything goes well return status 200 with no message
        /// </returns>
        [Authorize(Roles = "1, 2")]
        [HttpDelete("cancel-appointment/{appointmentId:int}")]
        public async Task<ActionResult> CancelAppointment(int appointmentId)
        {
            var isClientOrAdmin = await _service.IsAdminOrClient();
            if (!isClientOrAdmin) return Unauthorized("The user with userId from token are not a valid user");

            var result = await _service.CancelAppointment(appointmentId);
            return result switch
            {
                null => BadRequest("Appointment only can be canceled at least 24 hours before their completion"),
                false => StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorModel { ErrorCode = 500, Message = "Internal error deleting the comment" }),
                true => Ok()
            };
        }
        
        /// <summary>
        /// Get a statistics with the amount of done, booked and canceled appointments in the system
        /// </summary>
        /// <returns>
        /// If the user is not admin return status code 401 Unauthorized with custom message
        /// If something went wrong obtaining the statistics return status code 500 internal server error with custom message
        /// If everything goes well return a Dto with the statistics, the Dto have the following structure:
        /// CanceledAppointmentQuantity: Int with the amount of canceled appointments
        /// BookedAppointmentQuantity: Int with the amount of booked appointments
        /// DoneAppointmentQuantity: Int with the amount of done appointments
        /// </returns>
        [Authorize(Roles = "1")]
        [HttpGet("get-statistics")]
        public async Task<ActionResult<AppointmentStatisticsDto>> GetAppointmentStatistics()
        {
            var isAdmin = await _service.IsAdmin();
            if (!isAdmin) return Unauthorized("The user with userId from token are not a valid admin");

            var statistics = await _service.GetAppointmentStatistics();
            if (statistics is null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorModel { ErrorCode = 500, Message = "Internal error getting statistics" });
            return Ok(statistics);
        }
    }
}