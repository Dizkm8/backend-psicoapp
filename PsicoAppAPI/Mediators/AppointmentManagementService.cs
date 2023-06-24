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

    public AppointmentManagementService(IAppointmentService appointmentService, IAuthManagementService authService,
        IMapperService mapperService)
    {
        _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
        _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    public async Task<IEnumerable<AppointmentDto>?> GetAppointmentsByUser()
    {
        var userId = _authService.GetUserIdFromToken();
        if (userId is null) return null;

        var appointments = await _appointmentService.GetAppointmentsByUser(userId);
        var mappedAppointments = _mapperService.MapToAppointmentDto(appointments);
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
}