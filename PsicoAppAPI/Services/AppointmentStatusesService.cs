using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services;

public class AppointmentStatusesService : IAppointmentStatusesService
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentStatusesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
}