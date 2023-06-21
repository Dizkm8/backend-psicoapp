using PsicoAppAPI.Data;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories;

public class AppointmentStatusesRepository : IAppointmentStatusesRepository
{
    private readonly DataContext _context;

    public AppointmentStatusesRepository(DataContext context)
    {
        _context = context;
    }
}