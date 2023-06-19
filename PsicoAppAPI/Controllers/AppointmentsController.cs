using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsicoAppAPI.Controllers
{
    public class AppointmentsController : BaseApiController
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
        }

        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByUser(int userId)
        {
            var appointments = await _appointmentService.GetAppointmentsByUser(userId);
            var appointmentDtos = MapAppointmentsToDtos(appointments);
            return Ok(appointmentDtos);
        }

        private IEnumerable<AppointmentDto> MapAppointmentsToDtos(IEnumerable<Models.Appointment> appointments)
        {
            var appointmentDtos = new List<AppointmentDto>();
            foreach (var appointment in appointments)
            {
                var appointmentDto = new AppointmentDto
                {
                    Id = appointment.Id,
                    UserId = appointment.RequestingUserId,
                    SpecialistId = appointment.RequestedUserId,
                    BookedDate = appointment.BookedDate,
                    Status = GetAppointmentStatusName(appointment.AppointmentStatusId) // Agrega el nombre del estado de la cita
                };

                appointmentDtos.Add(appointmentDto);
            }

            return appointmentDtos;
        }

        private string GetAppointmentStatusName(int appointmentStatusId)
        {
            switch (appointmentStatusId)
            {
                case 1:
                    return "Done";
                case 2:
                    return "Booked";
                case 3:
                    return "Rejected";
                default:
                    return "Unknown";
            }
        }
    }
}
