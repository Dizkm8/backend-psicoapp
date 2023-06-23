using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators;

public class AppointmentManagementService : IAppointmentManagementService
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentManagementService(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
    }
}