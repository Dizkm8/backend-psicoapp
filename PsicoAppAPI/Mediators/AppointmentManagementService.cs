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

        // Change utc 0 to utc-4 or utc-3 (Chile)
        var utcAvailability = await _timeZoneService.ConvertToChileUTC(appoint.BookedDate);
        if (utcAvailability is null) return null;

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

        // Change utc 0 to utc-4 or utc-3 (Chile)
        var utcAvailability = await _timeZoneService.ConvertToChileUTC(appoint.BookedDate);
        if (utcAvailability is null) return null;

        var result = await _appointmentService.CancelAppointment(appointmentId);
        return result;
    }
}