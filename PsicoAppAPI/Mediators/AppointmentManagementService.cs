using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.Appointment;
using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators;

public class AppointmentManagementService : IAppointmentManagementService
{
    private readonly IAppointmentService _appointmentService;
    private readonly IAuthManagementService _authService;
    private readonly IMapperService _mapperService;
    private readonly ITimeZoneService _timeZoneService;

    public AppointmentManagementService(IAppointmentService appointmentService, IAuthManagementService authService,
        IMapperService mapperService, ITimeZoneService timeZoneService)
    {
        _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
        _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _timeZoneService = timeZoneService ?? throw new ArgumentNullException(nameof(timeZoneService));
    }

    public async Task<IEnumerable<ClientAppointmentDto>?> GetAppointmentsByClient()
    {
        var userId = _authService.GetUserIdFromToken();
        if (userId is null) return null;

        var appointments = await _appointmentService.GetAppointmentsByClient(userId);
        var mappedAppointments = _mapperService.MapToClientAppointmentDto(appointments);
        return mappedAppointments;
    }

    public async Task<IEnumerable<SpecialistAppointmentDto>?> GetAppointmentsBySpecialist()
    {
        var userId = _authService.GetUserIdFromToken();
        if (userId is null) return null;

        var appointments = await _appointmentService.GetAppointmentsBySpecialist(userId);
        var mappedAppointments = _mapperService.MapToSpecialistAppointmentDto(appointments);
        return mappedAppointments;
    }

    public async Task<IEnumerable<SpecialistAppointmentDto>?> GetAppointmentsBySpecialist(string specialistUserId)
    {
        var appointments = await _appointmentService.GetAppointmentsBySpecialist(specialistUserId);
        var mappedAppointments = _mapperService.MapToSpecialistAppointmentDto(appointments);
        return mappedAppointments;
    }

    public async Task<bool> IsUserClient()
    {
        var user = await _authService.GetUserEnabledAndClientFromToken();
        return user is not null;
    }

    public async Task<bool> IsAdminOrClient()
    {
        var admin = await _authService.GetUserEnabledAndAdminFromToken();
        var client = await _authService.GetUserEnabledAndClientFromToken();
        return admin is not null || client is not null;
    }

    public async Task<bool?> CancelAppointment(int appointmentId)
    {
        var admin = await _authService.GetUserEnabledAndAdminFromToken();
        var client = await _authService.GetUserEnabledAndClientFromToken();

        if (admin is not null) return await AdminCancelAppointment(appointmentId);

        if (client is not null) return await ClientCancelAppointment(client.Id, appointmentId);

        return false;
    }

    public async Task<bool> IsSpecialist()
    {
        var user = await _authService.GetUserEnabledAndSpecialistFromToken();
        return user is not null;
    }

    public async Task<bool> IsAdmin()
    {
        var user = await _authService.GetUserEnabledAndAdminFromToken();
        return user is not null;
    }

    public async Task<AppointmentStatisticsDto?> GetAppointmentStatistics()
    {
        var doneAmount = await _appointmentService.GetDoneAppointmentsQuantity();
        var canceledAmount = await _appointmentService.GetCanceledAppointmentsQuantity();
        var bookedAmount = await _appointmentService.GetBookedAppointmentsQuantity();
        if (doneAmount is null || canceledAmount is null || bookedAmount is null) return null;
        return new AppointmentStatisticsDto
        {
            CanceledAppointmentQuantity = (int)canceledAmount,
            BookedAppointmentQuantity = (int)bookedAmount,
            DoneAppointmentQuantity = (int)doneAmount
        };
    }

    /// <summary>
    /// Canceled an appointment from a client appointments
    /// </summary>
    /// <param name="userId">Id of the client</param>
    /// <param name="appointmentId">Id of the appointment</param>
    /// <returns>true if could be canceled</returns>
    private async Task<bool?> ClientCancelAppointment(string userId, int appointmentId)
    {
        var appointments = await _appointmentService.GetAppointmentsByClient(userId);
        var appoint = appointments.SingleOrDefault(a => a.Id == appointmentId);
        if (appoint is null) return false;

        if (appoint.BookedDate <= DateTime.Now.AddHours(-24)) return null;

        var result = await _appointmentService.CancelAppointment(appointmentId);
        return result;
    }

    /// <summary>
    /// Canceled an appointment from the system appointments
    /// </summary>
    /// <param name="appointmentId">Id of the appointment</param>
    /// <returns>true if could be canceled</returns>
    private async Task<bool?> AdminCancelAppointment(int appointmentId)
    {
        var appointments = await _appointmentService.GetAllAppointments();
        var appoint = appointments.SingleOrDefault(a => a.Id == appointmentId);
        if (appoint is null) return false;

        var result = await _appointmentService.CancelAppointment(appointmentId);
        return result;
    }
}